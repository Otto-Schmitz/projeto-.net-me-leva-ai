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
            var novoPassageiro = request.ToPassageiro();

            var response = new PassageiroDto();

            //if (!novoPassageiro.VerificaIdadeMinima())
            //{
            //    response.AddNotification(new Validations.Notification("Idade mínima é de 16 anos."));
            //    return response;
            //}

            //if (!novoPassageiro.VerificaCpf())
            //{
            //    response.AddNotification(new Validations.Notification("Cpf inválido."));
            //    return response;
            //}

            if (_passageiroRepository.ObterPorCpf(novoPassageiro.Cpf) != null)
            {
                response.AddNotification(new Validations.Notification("Passageiro já existe."));
                return response;
            }

            _passageiroRepository.Cadastrar(novoPassageiro);

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

        public PassageiroDto Alterar(Guid id, AlterarPassageiroRequest request)
        {
            var response = new PassageiroDto();

            var passageiroAtual = _passageiroRepository.Obter(id);

            if (passageiroAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro com o id {id} não encontrado."));
                return response;
            }

            var passageiroNovo = request.ToAlterarPassageiro();

            passageiroAtual.Alterar(passageiroNovo);

            response = passageiroAtual.ToPassageiroDto();

            return response;
        }

        public PassageiroDto Remover(Guid id)
        {
            var response = new PassageiroDto();

            var passageiro = _passageiroRepository.Obter(id);

            if (passageiro == null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro com o id {id} não encontrado."));
                return response;
            }

            _passageiroRepository.Remover(id);

            return response;
        }

        public PassageiroDto SacarSaldo(Guid id, ValorRequest request)
        {
            var response = new PassageiroDto();

            var passageiroAtual = _passageiroRepository.Obter(id);

            if (passageiroAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro com o id {id} não encontrado."));
                return response;
            }

            if (request.Valor > passageiroAtual.Saldo)
            {
                response.AddNotification(new Validations.Notification($"Saldo insuficiente para realizar esse saque."));
                return response;
            }

            passageiroAtual.SacarSaldo(request.Valor);

            response = passageiroAtual.ToPassageiroDto();

            return response;
        }

        public PassageiroDto DepositarSaldo(Guid id, ValorRequest request)
        {
            var response = new PassageiroDto();

            var passageiroAtual = _passageiroRepository.Obter(id);

            if (passageiroAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Passageiro com o id {id} não encontrado."));
                return response;
            }

            if (request.Valor <= 0)
            {
                response.AddNotification(new Validations.Notification($"Valor depositado deve ser maior que zero."));
                return response;
            }

            passageiroAtual.DepositarSaldo(request.Valor);

            response = passageiroAtual.ToPassageiroDto();

            return response;
        }
    }
}
