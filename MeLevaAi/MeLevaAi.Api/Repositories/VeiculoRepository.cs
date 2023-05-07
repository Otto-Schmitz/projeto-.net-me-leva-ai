using MeLevaAi.Api.Domains;
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

            if (veiculo == null)
                return false;

            return _veiculos.Remove(veiculo);
        }

        public void Atualizar(Veiculo veiculo)
        {
            Remover(veiculo.Id);
            Adicionar(veiculo);
        }
        public Veiculo? ObterPorMotorista(Guid motoristaId)
        {
            return _veiculos.FirstOrDefault(v => v.MotoristaId == motoristaId);
        }

        public Veiculo? ObterAleatorio()
        {
            var random = new Random().Next(0, _veiculos.Count - 1);

            return _veiculos[random];
        }

    }
}
