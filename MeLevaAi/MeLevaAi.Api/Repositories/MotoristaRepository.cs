using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Domains;
using System.Xml.Linq;

namespace MeLevaAi.Api.Repositories
{
    public class MotoristaRepository
    {
        private static readonly List<Motorista> _motoristas = new();

        public IEnumerable<Motorista> Listar()
            => _motoristas;

        public Motorista? Obter(Guid id)
            => _motoristas.FirstOrDefault(v => v.Id == id);

        public void Adicionar(Motorista motorista)
        {
            _motoristas.Add(motorista);
        }

        public bool Remover(Guid id)
        {
            var motorista = Obter(id);

            if (motorista is null)
                return false;

            return _motoristas.Remove(motorista);
        }

        public void Atualizar(Motorista motorista)
        {
            var index = _motoristas.FindIndex(v => v.Id == motorista.Id);

            if (index != -1)
            {
                _motoristas[index] = motorista;
            }
        }
    }
}
