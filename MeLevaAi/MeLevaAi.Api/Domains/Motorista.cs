using MeLevaAi.Api.Domain;
using System;

namespace MeLevaAi.Api.Domains
{
    public partial class Motorista : Pessoa
    {
        public Motorista(string nome, string email, DateTime dataNascimento, string cpf, CarteiraDeHabilitacao carteiraDeHabilitacao)
            : base(nome, email, dataNascimento, cpf)
        {
            CarteiraDeHabilitacao = carteiraDeHabilitacao;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public CarteiraDeHabilitacao CarteiraDeHabilitacao { get; set; }

        public Motorista Alterar(Motorista motorista)
        {
            Nome = motorista.Nome;
            Email = motorista.Email;
            DataNascimento = motorista.DataNascimento;
            Cpf = motorista.Cpf;
            CarteiraDeHabilitacao = motorista.CarteiraDeHabilitacao;

            return this;
        }

        public override bool VerificaIdadeMinima()
        {
            int idadeMinima = 18;
            DateTime dataAtual = DateTime.Now;

            int idade = dataAtual.Year - DataNascimento.Year;
            if (DataNascimento > dataAtual.AddYears(-idade))
                --idade;

            return idade >= idadeMinima;
        }

        public Motorista SacarSaldo(double valor)
        {
            Saldo -= valor;
            return this;
        }

        public Motorista DepositarSaldo(double valor)
        {
            Saldo += valor;
            return this;
        }
    }
}
