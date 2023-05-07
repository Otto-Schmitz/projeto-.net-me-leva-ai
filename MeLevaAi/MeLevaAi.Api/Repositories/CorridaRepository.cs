using MeLevaAi.Api.Domains;
using System.Xml.Linq;

namespace MeLevaAi.Api.Repositories
{
    public class CorridaRepository
    {
        private static readonly List<Corrida> _corridas= new();

        public IEnumerable<Corrida> Listar()
            => _corridas;

        public Corrida? Obter(Guid id)
            => _corridas.FirstOrDefault(v => v.CorridaID == id);

        public void Adicionar(Corrida corrida)
        {
            _corridas.Add(corrida);
        }

        public void Alterar(Corrida corrida)
        {
            Remover(corrida.CorridaID);
            Adicionar(corrida);
        }

        public bool Remover(Guid id)
        {
            var corrida = Obter(id);

            if (corrida == null)
                return false;

            return _corridas.Remove(corrida);
        }
    }
}
