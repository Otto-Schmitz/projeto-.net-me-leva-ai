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

            var tempoEstimadoDestino = (distanciaEmKm / 30 * 60 * 60);

            var valorEstimado = tempoEstimadoDestino * 0.2;

            corrida.AtualizarValorEstimado(valorEstimado);

            corrida.AtualizarStatusCorrida(StatusCorrida.Iniciada);

            corrida.AtualizarTempoEstimadoDestino(tempoEstimadoDestino);

            return corrida.ToIniciarCorridaDto();
        }

        public CorridaDto AvaliarPassageiro(Guid corridaId, AvaliarPessoaRequest request)
        {
            var response = new CorridaDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida is null)
            {
                response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} não encontrada."));
                return response;
            }

            var passageiro = _passageiroRepository.Obter(corrida.PassageiroId);

            if (request.Nota < 1 || request.Nota > 5)
            {
                response.AddNotification(new Validations.Notification($"A nota deve estar entre 1 e 5."));
                return response;
            }

            var avaliacao = new Avaliacao(pessoaId: passageiro.Id, corridaId: corrida.CorridaID, nota: request.Nota, descricao: request.Descricao);

            passageiro.Avaliacoes.Add(avaliacao);

            return response;
        }

        public CorridaDto AvaliarMotorista(Guid corridaId, AvaliarPessoaRequest request)
        {
            var response = new CorridaDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida is null)
            {
                response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} não encontrada."));
                return response;
            }

            var motorista = _motoristaRepository.Obter(corrida.Veiculo.MotoristaId);

            if (request.Nota < 1 || request.Nota > 5)
            {
                response.AddNotification(new Validations.Notification($"A nota deve estar entre 1 e 5."));
                return response;
            }

            var avaliacao = new Avaliacao(pessoaId: motorista.Id, corridaId: corrida.CorridaID, nota: request.Nota, descricao: request.Descricao);

            motorista.Avaliacoes.Add(avaliacao);

            return response;
        }
    }
}