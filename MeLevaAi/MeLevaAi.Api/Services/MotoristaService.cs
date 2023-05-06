﻿using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class MotoristaService
    {
        private readonly MotoristaRepository _motoristaRepository;

        public MotoristaService()
        {
            _motoristaRepository = new MotoristaRepository();
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

            _motoristaRepository.Adicionar(novoMotorista);

            return novoMotorista.ToMotoristaDto();
        }

        public MotoristaDto Alterar(Guid id, AdicionarMotoristaRequest request)
        {
            var response = new MotoristaDto();

            var motoristaAtual = _motoristaRepository.Obter(id);

            if (motoristaAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            var motoristaNovo = request.ToMotorista();

            motoristaAtual.Alterar(motoristaNovo);

            response = motoristaAtual.ToMotoristaDto();

            return response;
        }

        public MotoristaDto Remover(Guid id)
        {
            var response = new MotoristaDto();

            var motorista = _motoristaRepository.Obter(id);

            if (motorista is null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            _motoristaRepository.Remover(id);

            return response;
        }

        public MotoristaDto SacarSaldo(Guid id, ValorRequest request)
        {
            var response = new MotoristaDto();

            var motoristaAtual = _motoristaRepository.Obter(id);

            if (motoristaAtual is null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
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

            if (motoristaAtual is null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {id} não encontrado."));
                return response;
            }

            motoristaAtual.DepositarSaldo(request.Valor);

            response = motoristaAtual.ToMotoristaDto();

            return response;
        }
    }
}