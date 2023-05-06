using MeLevaAi.Api.Domain;
using System.Text.RegularExpressions;

namespace MeLevaAi.Api.Domains
{
    public partial class Motorista : Pessoa
    {
        public Motorista(string nome, string email, DateOnly dataNascimento, string cpf) : base(nome, email, dataNascimento, cpf) { }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Categoria Categoria { get; init; }

        public Motorista Alterar(Motorista motorista)
        {
            DataNascimento = motorista.DataNascimento;
            Nome = motorista.Nome;

            return this;
        }

        public override bool VerificaIdadeMinima()
        {
            int idadeMinima = 18;
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            int idade = dataAtual.Year - DataNascimento.Year;
            if (DataNascimento > dataAtual.AddYears(-idade))
                --idade;

            return idade >= idadeMinima;
        }

    }
}
