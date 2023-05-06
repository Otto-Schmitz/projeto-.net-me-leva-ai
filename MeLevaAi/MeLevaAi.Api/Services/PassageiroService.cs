using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class PassageiroService
    {
        private readonly PassageiroRepository _repository;

        public PassageiroService() => _repository = new PassageiroRepository();

        public PassageiroDto Cadastrar(CadastrarPassageiroRequest request)
        {
            var passageiro = request.ToPassageiro();

            var response = new PassageiroDto();

            if (_repository.Obter(passageiro.Id) != null) {
                response.AddNotification(new Validations.Notification("Passageiro ja existe"));
                return response;
            }

            var novoPassageiro = _repository.Cadastrar(passageiro);

            return novoPassageiro.ToPassageiroDto();
        }
    }
}
