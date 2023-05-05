using System.Text.RegularExpressions;

namespace MeLevaAi.Api.Domains
{
    public partial class Passageiro : Pessoa
    {
        public Passageiro(string nome, string email, DateOnly dataNascimento, string cpf) : base(nome, email, dataNascimento, cpf) { }

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
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            int idade = dataAtual.Year - DataNascimento.Year;
            if (DataNascimento > dataAtual.AddYears(-idade))
                --idade;

            return idade >= idadeMinima;
        }

    }
}
