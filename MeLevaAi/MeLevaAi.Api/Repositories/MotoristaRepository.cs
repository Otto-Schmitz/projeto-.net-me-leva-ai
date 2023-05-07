using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Domains;
using System.Xml.Linq;

namespace MeLevaAi.Api.Repositories
{
    public class MotoristaRepository
    {
        private static readonly List<Motorista> _motoristas = new();

        public IEnumerable<Motorista> Listar()
            => _motoristas;

        public Motorista? Obter(Guid? id)
            => _motoristas.FirstOrDefault(v => v.Id == id);

        public Motorista? ObterPorCpf(string cpf)
            => _motoristas.FirstOrDefault(v => v.Cpf == cpf);

        public void Cadastrar(Motorista motorista)
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
    }
}
