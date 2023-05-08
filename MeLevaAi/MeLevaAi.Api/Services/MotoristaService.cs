using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Services
{
    public class MotoristaService
    {
        private readonly MotoristaRepository _motoristaRepository;
        private readonly VeiculoRepository _veiculoRepository;

        public MotoristaService()
        {
            _motoristaRepository = new MotoristaRepository();
            _veiculoRepository = new VeiculoRepository();
        }

        public IEnumerable<MotoristaDto> Listar()
        {
            var motoristas = _motoristaRepository.Listar();

            return motoristas.Select(v => v.ToMotoristaDto());
        }

        public MotoristaDto Obter(Guid id)
        {
            var response = new MotoristaDto();

            var motorista = _motoristaRepository.Obter(id);

            if (motorista == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            return motorista.ToMotoristaDto();
        }

        public MotoristaDto Adicionar(AdicionarMotoristaRequest request)
        {
            var novoMotorista = request.ToMotorista();

            var response = new MotoristaDto();

            if (!novoMotorista.VerificaIdadeMinima())
            {
               response.AddNotification(new Validations.Notification("Idade mínima é de 18 anos."));
               return response;
            }

            if (!novoMotorista.VerificaCpf())
            {
               response.AddNotification(new Validations.Notification("Cpf inválido."));
               return response;
            }

            _motoristaRepository.Cadastrar(novoMotorista);

            return novoMotorista.ToMotoristaDto();
        }

        public MotoristaDto Alterar(Guid id, AlterarMotoristaRequest request)
        {
            var response = new MotoristaDto();

            var motoristaAtual = _motoristaRepository.Obter(id);

            if (motoristaAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            var motoristaNovo = request.ToAlterarMotorista();

            motoristaAtual.Alterar(motoristaNovo);

            response = motoristaAtual.ToMotoristaDto();

            return response;
        }

        public MotoristaDto Remover(Guid id)
        {
            var response = new MotoristaDto();

            var motorista = _motoristaRepository.Obter(id);

            if (motorista == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            var veiculo = _veiculoRepository.ObterPorMotorista(id);

            if (veiculo != null)
            {
                response.AddNotification(new Validations.Notification($"Não é possível remover o motorista com o id {id}, pois ele possui um veículo associado."));
                return response;
            }

            _motoristaRepository.Remover(id);

            return response;
        }

        public MotoristaDto SacarSaldo(Guid id, ValorRequest request)
        {
            var response = new MotoristaDto();

            var motoristaAtual = _motoristaRepository.Obter(id);

            if (motoristaAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            if (request.Valor > motoristaAtual.Saldo)
            {
                response.AddNotification(new Validations.Notification($"Saldo insuficiente para realizar esse saque."));
                return response;
            }

            motoristaAtual.SacarSaldo(request.Valor);

            response = motoristaAtual.ToMotoristaDto();

            return response;
        }

        public MotoristaDto DepositarSaldo(Guid id, ValorRequest request)
        {
            var response = new MotoristaDto();

            var motoristaAtual = _motoristaRepository.Obter(id);

            if (motoristaAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            if (request.Valor <= 0)
            {
                response.AddNotification(new Validations.Notification($"Valor depositado deve ser maior que zero."));
                return response;
            }

            motoristaAtual.DepositarSaldo(request.Valor);

            response = motoristaAtual.ToMotoristaDto();

            return response;
        }

    }
}