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

        private readonly double _multiplicadorValor = 0.2;


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

            if (corrida == null)
            {
                response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} não encontrada."));
                return response;
            }

            if (corrida.StatusCorrida != StatusCorrida.Solicitada)
            {
                response.AddNotification(new Validations.Notification("Corrida não solicitada."));
                return response;
            }

            var distanciaEmKm = CalculadorDistancia.CalcularDistancia(corrida.PontoInicial, corrida.PontoFinal);

            var tempoEstimadoDestino = (distanciaEmKm / 30 * 60 * 60);

            var valorEstimado = tempoEstimadoDestino * _multiplicadorValor;

            corrida.AtualizarValorEstimado(valorEstimado);

            corrida.AtualizarStatusCorrida(StatusCorrida.Iniciada);

            corrida.AtualizarTempoEstimadoDestino(tempoEstimadoDestino);

            corrida.AdicionarTempoInicial(DateTime.Now);

            _corridaRepository.Alterar(corrida);

            return corrida.ToIniciarCorridaDto();
        }

        public FinalizarCorridaDto Finalizar(Guid corridaId)
        {
            var response = new FinalizarCorridaDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida == null) {
                response.AddNotification(new Validations.Notification($"Corrida com o id {corridaId} não encontrada."));
                return response;
            }

            if (corrida.StatusCorrida != StatusCorrida.Iniciada)
            {
                response.AddNotification(new Validations.Notification("Corrida não inicializada."));
                return response;
            }

            var passageiro = _passageiroRepository.Obter(corrida.PassageiroId);
            var motorista = _motoristaRepository.Obter(corrida.Veiculo.MotoristaId);

            if (passageiro == null)
            {
                response.AddNotification(new Validations.Notification("Passageiro não encontrada."));
                return response;
            }

            if (motorista == null)
            {
                response.AddNotification(new Validations.Notification("Motorista não encontrada."));
                return response;
            }

            var tempoCorrida = CalcularTempoFinal(corrida.TempoInicial);

            var valor = CalcularValorFinal(tempoCorrida);

            if (valor > passageiro.Saldo)
            {
                response.AddNotification(new Validations.Notification($"Saldo insuficiente. Valor à ser pago R${valor}"));
                return response;
            }

            passageiro.SacarSaldo(valor);
            motorista.DepositarSaldo(valor);

            corrida.AdicionarValorFinal(valor);
            corrida.AdicionarTempoFinal(tempoCorrida.ToString());
            corrida.AtualizarStatusCorrida(StatusCorrida.Finalizada);

            _corridaRepository.Alterar(corrida);
            
            return corrida.ToFinalizarCorridaDto();
        }

        public double CalcularTempoFinal(DateTime tempoInicial)
        {
            return (DateTime.Now - tempoInicial).TotalSeconds;
        }

        public double CalcularValorFinal(double tempo)
        {
            return tempo * _multiplicadorValor;
        }

        public CorridaDto AvaliarPassageiro(Guid corridaId, AvaliarPessoaRequest request)
        {
            var response = new CorridaDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida == null)
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

            var avaliacao = new Avaliacao(pessoaId: passageiro.Id, corridaId: corrida.CorridaId, nota: request.Nota, descricao: request.Descricao);

            passageiro.Avaliacoes.Add(avaliacao);

            return response;
        }

        public CorridaDto AvaliarMotorista(Guid corridaId, AvaliarPessoaRequest request)
        {
            var response = new CorridaDto();

            var corrida = _corridaRepository.Obter(corridaId);

            if (corrida == null)
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

            var avaliacao = new Avaliacao(pessoaId: motorista.Id, corridaId: corrida.CorridaId, nota: request.Nota, descricao: request.Descricao);

            motorista.Avaliacoes.Add(avaliacao);

            return response;
        }
    }
}