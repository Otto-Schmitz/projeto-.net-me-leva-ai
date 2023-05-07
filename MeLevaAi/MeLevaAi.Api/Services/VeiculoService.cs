using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;
using MeLevaAi.Api.Validations;
using System.Security.Cryptography;

namespace MeLevaAi.Api.Services
{
    public class VeiculoService
    {
        private readonly VeiculoRepository _veiculoRepository;

        private readonly MotoristaRepository _motoristaRepository;

        public VeiculoService()
        {
            _veiculoRepository = new VeiculoRepository();

            _motoristaRepository = new MotoristaRepository();
        }

        public IEnumerable<VeiculoDto> Listar()
        {
            var veiculos = _veiculoRepository.Listar();

            return veiculos.Select(v => v.ToVeiculoDto());
        }

        public VeiculoDto Obter(Guid id)
        {
            var response = new VeiculoDto();

            var veiculo = _veiculoRepository.Obter(id);

            if (veiculo == null)
            {
                response.AddNotification(new Validations.Notification($"Veículo com o id {id} não encontrado."));

                return response;
            }

            return veiculo.ToVeiculoDto();
        }

        public VeiculoDto Adicionar(AdicionarVeiculoRequest request)
        {
            var novoVeiculo = request.ToVeiculo();

            var motorista = _motoristaRepository.Obter(request.MotoristaId);

            if (motorista == null)
            {
                var response = new VeiculoDto();

                response.AddNotification(new Validations.Notification($"Motorista com o id {request.MotoristaId} não encontrado."));

                return response;
            }

            if (!VeiculoValidation.VerificarCategoria(novoVeiculo, motorista))
            {
                var response = new VeiculoDto();

                response.AddNotification(new Validations.Notification("A categoria do veículo não é compatível com a categoria da carteira de habilitação do motorista."));

                return response;
            }

            _veiculoRepository.Adicionar(novoVeiculo);

            return novoVeiculo.ToVeiculoDto();
        }


        public VeiculoDto Alterar(Guid id, AlterarVeiculoRequest request)
        {
            var response = new VeiculoDto();

            var veiculoAtual = _veiculoRepository.Obter(id);

            if (veiculoAtual == null)
            {
                response.AddNotification(new Validations.Notification($"Veículo com o id {id} não encontrado."));

                return response;
            }

            var motorista = _motoristaRepository.Obter(request.MotoristaId);

            var veiculoAlterado = request.ToAlterarVeiculo();

            if (motorista == null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {request.MotoristaId} não encontrado."));

                return response;
            }

            if (!VeiculoValidation.VerificarCategoria(veiculoAlterado, motorista))
            {
                response.AddNotification(new Validations.Notification("A categoria do veículo não é compatível com a categoria da carteira de habilitação do motorista."));

                return response;
            }

            veiculoAtual.Alterar(veiculoAlterado);

            return veiculoAlterado.ToVeiculoDto();
        }

        public VeiculoDto Remover(Guid id)
        {
            var response = new VeiculoDto();

            var veiculo = _veiculoRepository.Obter(id);

            if (veiculo == null)
            {
                response.AddNotification(new Validations.Notification($"Veículo com o id {id} não encontrado."));

                return response;
            }

            _veiculoRepository.Remover(id);

            return response;
        }


    }
}