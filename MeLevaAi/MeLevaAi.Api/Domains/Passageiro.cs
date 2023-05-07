using System.Text.RegularExpressions;

namespace MeLevaAi.Api.Domains
{
    public partial class Passageiro : Pessoa
    {
        public List<Corrida> Corridas { get; init; } = new List<Corrida>();

        public Passageiro(string nome, string email, DateTime dataNascimento, string cpf) 
            : base(nome, email, dataNascimento, cpf) { }

        public List<Avaliacao> Avaliacoes { get; set; }

        public Passageiro(string nome, string email, DateTime dataNascimento, string cpf)
            : base(nome, email, dataNascimento, cpf)
        {
            Avaliacoes = new List<Avaliacao>();
        }

        public Passageiro Alterar(Passageiro passageiro)
        {
            Nome = passageiro.Nome;
            Email = passageiro.Email;
            DataNascimento = passageiro.DataNascimento;
            Cpf = passageiro.Cpf;

            return this;
        }

        public override bool VerificaIdadeMinima()
        {
            int idadeMinima = 16;
            DateTime dataAtual = DateTime.Now;
            int idade = dataAtual.Year - DataNascimento.Year;

            if (DataNascimento > dataAtual.AddYears(-idade))
                --idade;

            return idade >= idadeMinima;
        }

        public Passageiro SacarSaldo(double valor)
        {
            Saldo -= valor;
            return this;
        }

        public Passageiro DepositarSaldo(double valor)
        {
            Saldo += valor;
            return this;
        }

        public void AdicionarCorrida(Corrida corrida)
        {
            Corridas.Add(corrida);
        }

        public void RemoverCorrida(Corrida corrida)
        {
            Corridas.Remove(corrida);
        }

        public Corrida? ObterCorrida(Guid id)
            =>  Corridas.FirstOrDefault(v => v.CorridaID == id);
        

        public void AlterarCorrida(Corrida corrida)
        {
            RemoverCorrida(ObterCorrida(corrida.CorridaID));
            AdicionarCorrida(corrida);
        }

    }
}
