using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Repositories
{
    public class PassageiroRepository
    {
        private static readonly List<Passageiro> _passageiros = new();

        public IEnumerable<Passageiro> Listar()
    => _passageiros;

        public Passageiro? Obter(Guid? id)
            => (from a in _passageiros where a.Id == id select a).FirstOrDefault();

        public Passageiro? ObterPorCpf(string cpf) 
            => (from a in _passageiros where a.Cpf == cpf select a).FirstOrDefault();

        public Passageiro Cadastrar(Passageiro passageiro)
        {
            _passageiros.Add(passageiro);

            return passageiro;
        }

        public bool Remover(Guid id)
        {
            var passageiro = Obter(id);

            if (passageiro is null)
                return false;

            return _passageiros.Remove(passageiro);
        }
    }
}
