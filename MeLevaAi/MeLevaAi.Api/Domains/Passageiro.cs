using System.Text.RegularExpressions;

namespace MeLevaAi.Api.Domains
{
    public partial class Passageiro : Pessoa
    {
        public Passageiro(Guid id, string nome, string email, DateTime dataNascimento, string cpf) : base(nome, email, dataNascimento, cpf) {
            Id = id;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Passageiro Alterar(Passageiro passageiro)
        {
            DataNascimento = passageiro.DataNascimento;
            Nome = passageiro.Nome;

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

    }
}
