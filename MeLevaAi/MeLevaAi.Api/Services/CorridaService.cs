using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class CorridaService
    {
        private readonly CorridaRepository _corridaRepository;
        private readonly PassageiroRepository _passageiroRepository;
        private readonly MotoristaRepository _motoristaRepository; 


        public CorridaService()
        {
            _corridaRepository = new CorridaRepository();
            _passageiroRepository = new PassageiroRepository();
            _motoristaRepository = new MotoristaRepository();
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