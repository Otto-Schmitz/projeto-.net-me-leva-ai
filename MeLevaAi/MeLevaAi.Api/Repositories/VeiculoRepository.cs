using MeLevaAi.Api.Domain;
using System.Xml.Linq;

namespace MeLevaAi.Api.Repositories
{
    public class VeiculoRepository
    {
        private static readonly List<Veiculo> _veiculos = new();

        public IEnumerable<Veiculo> Listar()
            => _veiculos;

        public Veiculo? Obter(Guid id)
            => _veiculos.FirstOrDefault(v => v.Id == id);

        public void Adicionar(Veiculo veiculo)
        {
            _veiculos.Add(veiculo);
        }

        public bool Remover(Guid id)
        {
            var veiculo = Obter(id);

            if (veiculo is null)
                return false;

            return _veiculos.Remove(veiculo);
        }

        public void Atualizar(Veiculo veiculo)
        {
            var index = _veiculos.FindIndex(v => v.Id == veiculo.Id);

            if (index != -1)
            {
                _veiculos[index] = veiculo;
            }
        }
    }
}
