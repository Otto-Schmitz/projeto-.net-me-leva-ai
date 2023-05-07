using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class VeiculoService
    {
        private readonly VeiculoRepository _veiculoRepository;
        private readonly MotoristaRepository _motoristaRepository;

        public VeiculoService(VeiculoRepository veiculoRepository, MotoristaRepository motoristaRepository)
        {
            _veiculoRepository = veiculoRepository;
            _motoristaRepository = motoristaRepository;
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

            if (veiculo is null)
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

            var response = new VeiculoDto();

            if (motorista is null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {request.MotoristaId} não encontrado."));
                return response;
            }

            _veiculoRepository.Adicionar(novoVeiculo);

            return novoVeiculo.ToVeiculoDto();
        }

        public VeiculoDto Alterar(Guid id, AdicionarVeiculoRequest request)
        {
            var response = new VeiculoDto();

            var veiculoAtual = _veiculoRepository.Obter(id);

            var motorista = _motoristaRepository.Obter(request.MotoristaId);

            if (motorista is null)
            {
                response.AddNotification(new Validations.Notification($"Motorista com o id {request.MotoristaId} não encontrado."));
                return response;
            }

            if (veiculoAtual is null)
            {
                response.AddNotification(new Validations.Notification($"Veículo com o id {id} não encontrado."));
                return response;
            }

            var veiculoNovo = request.ToVeiculo();

            veiculoAtual.Alterar(veiculoNovo);

            return response;
        }

        public VeiculoDto Remover(Guid id)
        {
            var response = new VeiculoDto();

            var veiculo = _veiculoRepository.Obter(id);

            if (veiculo is null)
            {
                response.AddNotification(new Validations.Notification($"Veículo com o id {id} não encontrado."));
                return response;
            }

            _veiculoRepository.Remover(id);

            return response;
        }
    }
}