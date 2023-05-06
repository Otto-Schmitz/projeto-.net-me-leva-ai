using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class PassageiroService
    {
        private readonly PassageiroRepository _passageiroRepository;

        public PassageiroService() => _passageiroRepository = new PassageiroRepository();

        public PassageiroDto Adicionar(AdicionarPassageiroRequest request)
        {
            var passageiro = request.ToPassageiro();
            var response = new PassageiroDto();

            if (!passageiro.VerificaIdadeMinima())
            {
                response.AddNotification(new Validations.Notification("Idade mínima é de 16 anos."));
                return response;
            }

            if (!passageiro.VerificaCpf())
            {
                response.AddNotification(new Validations.Notification("Cpf inválido."));
                return response;
            }

            if (_passageiroRepository.Obter(passageiro.Id) != null) {
                response.AddNotification(new Validations.Notification("Passageiro já existe."));
                return response;
            }

            var novoPassageiro = _passageiroRepository.Cadastrar(passageiro);

            return novoPassageiro.ToPassageiroDto();
        }

        public IEnumerable<PassageiroDto> Listar()
        {
            var passageiros = _passageiroRepository.Listar();

            return passageiros.Select(p => p.ToPassageiroDto());
        }

        public PassageiroDto Obter(Guid id)
        {
            var response = new PassageiroDto();

            var passageiro = _passageiroRepository.Obter(id);

            if (passageiro == null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro com o id {id} não encontrado."));
                return response;
            }

            return passageiro.ToPassageiroDto();
        }

        public PassageiroDto Alterar(Guid id, AdicionarPassageiroRequest request)
        {
            var response = new PassageiroDto();

            var passageiroAtual = _passageiroRepository.Obter(id);

            if (passageiroAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro com o id {id} não encontrado."));
                return response;
            }

            var passageiroNovo = request.ToPassageiro();

            passageiroAtual.Alterar(passageiroNovo);

            response = passageiroAtual.ToPassageiroDto();

            return response;
        }

    }
}
