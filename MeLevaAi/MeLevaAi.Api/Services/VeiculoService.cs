using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Mappers;
using MeLevaAi.Api.Repositories;

namespace MeLevaAi.Api.Services
{
    public class VeiculoService
    {
        private readonly VeiculoRepository _veiculoRepository;

        public VeiculoService()
        {
            _veiculoRepository = new VeiculoRepository();
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

            _veiculoRepository.Adicionar(novoVeiculo);

            return novoVeiculo.ToVeiculoDto();
        }

        public VeiculoDto Alterar(Guid id, AdicionarVeiculoRequest request)
        {
            var response = new VeiculoDto();

            var veiculoAtual = _veiculoRepository.Obter(id);

            if (veiculoAtual is null)
            {
                response.AddNotification(new Validations.Notification($"Veículo com o id {id} não encontrado."));
                return response;
            }

            var veiculoNovo = request.ToVeiculo();

            veiculoAtual.Alterar(veiculoNovo);

            response.Veiculo = veiculoAtual.ToVeiculoDto();

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