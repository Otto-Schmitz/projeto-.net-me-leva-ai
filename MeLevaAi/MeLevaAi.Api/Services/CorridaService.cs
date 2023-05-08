using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Helpers;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class CorridaService
    {
        private readonly CorridaRepository _corridaRepository;

        private readonly VeiculoRepository _veiculoRepository;

        private readonly MotoristaRepository _motoristaRepository;

        private readonly PassageiroRepository _passageiroRepository;

        private readonly double VALOR_POR_SEGUNDO = 0.2;

        private readonly double VELOCIDADE_EM_KMH = 30;

        private readonly double SEGUNDOS_EM_1H = 60 * 60;

        private readonly double NOTA_MINIMA = 1;

        private readonly double NOTA_MAXIMA = 5;

        public CorridaService()
        {
            _corridaRepository = new CorridaRepository();
            _veiculoRepository = new VeiculoRepository();
            _motoristaRepository = new MotoristaRepository();
            _passageiroRepository = new PassageiroRepository();
        }

        public ChamarCorridaDto Chamar(ChamarCorridaRequest request)
        {
            var response = new ChamarCorridaDto();

            var veiculo = ChamarVeiculo();

            if (veiculo == null)
            {
                response.AddNotification(new Validations.Notification("Nenhum veículo disponível foi encontrados."));
                return response;
            }

            var motorista = _motoristaRepository.Obter(veiculo.MotoristaId);

            var passageiro = _passageiroRepository.Obter(request.PassageiroId);

            if (passageiro == null)
            {
                response.AddNotification(new Validations.Notification("Passageiro inválida."));
                return response;
            }
            if (passageiro.EmCorrida)
            {
                response.AddNotification(new Validations.Notification("Passageiro em corrida."));
                return response;
            }

            var corrida = request.ToCorrida(passageiro, veiculo);

            _corridaRepository.Adicionar(corrida);

            passageiro.AdicionarCorrida(corrida);
            motorista.AdicionarCorrida(corrida);
            passageiro.IniciarCorrida();
            motorista.IniciarCorrida();

            response = corrida.ToChamarCorridaDto();

            return response;
        }

        private Veiculo? ChamarVeiculo()
        {
            var veiculos = _veiculoRepository.Listar().ToArray();

            foreach (var veiculo in veiculos)
            {
                var motorista = _motoristaRepository.Obter(veiculo.MotoristaId.GetValueOrDefault());

                if (motorista.CarteiraDeHabilitacao.DataVencimento < DateTime.Now)
                    return null;
                if (motorista.EmCorrida)
                    return null;

                return veiculo;
            }

            return null;
        }

        public IniciarCorridaDto Iniciar(Guid corridaId)
        {
            var response = new IniciarCorridaDto();
            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida is null)
            {
                response.AddNotification(new Validations.Notification("Corrida não encontrada."));
                return response;
            }

            var distanciaEmKm = CalculadorDistancia.CalcularDistancia(corrida.PontoInicial, corrida.PontoFinal);
            var tempoEstimadoDestino = (distanciaEmKm / VELOCIDADE_EM_KMH * SEGUNDOS_EM_1H);
            var valorEstimado = tempoEstimadoDestino * VALOR_POR_SEGUNDO;

            corrida.AtualizarValorEstimado(valorEstimado);
            corrida.AtualizarStatusCorrida(StatusCorrida.Iniciada);
            corrida.AtualizarTempoEstimadoDestino(tempoEstimadoDestino);

            return corrida.ToIniciarCorridaDto();
        }

        public AvaliarPassageiroDto AvaliarPassageiro(Guid corridaId, AvaliarPessoaRequest request)
        {
            var response = new AvaliarPassageiroDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida is null)
            {
                response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} não encontrada."));
                return response;
            }

            //if (corrida.StatusCorrida is not StatusCorrida.Finalizada)
            //{
            //    response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} precisa ser finalizada para avaliar o passageiro."));
            //    return response;
            //}

            if (request.Nota < NOTA_MINIMA || request.Nota > NOTA_MAXIMA)
            {
                response.AddNotification(new Validations.Notification($"A nota deve estar entre {NOTA_MINIMA} e {NOTA_MAXIMA}."));
                return response;
            }

            var passageiro = _passageiroRepository.Obter(corrida.PassageiroId);

            if (passageiro is null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro não encontrado."));
                return response;
            }

            var avaliacao = new Avaliacao(pessoaId: passageiro.Id, corridaId: corrida.CorridaID, nota: request.Nota, descricao: request.Descricao);

            corrida.AtualizarAvaliacaoPassageiro(avaliacao);

            passageiro.Avaliacoes.Add(avaliacao);

            response = corrida.ToAvaliarPassageiroDto(passageiro);

            return response;
        }

        public AvaliarMotoristaDto AvaliarMotorista(Guid corridaId, AvaliarPessoaRequest request)
        {
            var response = new AvaliarMotoristaDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida is null)
            {
                response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} não encontrada."));
                return response;
            }

            //if (corrida.StatusCorrida is not StatusCorrida.Finalizada)
            //{
            //    response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} precisa ser finalizada para avaliar o motorista."));
            //    return response;
            //}

            var motorista = _motoristaRepository.Obter(corrida.Veiculo.MotoristaId);

            if (motorista is null)
            {
                response.AddNotification(new Validations.Notification($"Motorista não encontrado."));
                return response;
            }

            if (request.Nota < NOTA_MINIMA || request.Nota > NOTA_MAXIMA)
            {
                response.AddNotification(new Validations.Notification($"A nota deve estar entre {NOTA_MINIMA} e {NOTA_MAXIMA}."));
                return response;
            }

            var avaliacao = new Avaliacao(pessoaId: motorista.Id, corridaId: corrida.CorridaID, nota: request.Nota, descricao: request.Descricao);

            corrida.AtualizarAvaliacaoMotorista(avaliacao);

            motorista.Avaliacoes.Add(avaliacao);

            response = corrida.ToAvaliarMotoristaDto(motorista);

            return response;
        }
    }
}