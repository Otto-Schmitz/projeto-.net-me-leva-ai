using MeLevaAi.Api.Domains;
using System;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Domains
{
    public partial class Motorista : Pessoa
    {
        public CarteiraDeHabilitacao CarteiraDeHabilitacao { get; set; }

        public List<Corrida> Corridas { get; init; } = new List<Corrida>();

        public List<Avaliacao> Avaliacoes { get; set; }

        public Motorista(string nome, string email, DateTime dataNascimento, string cpf, CarteiraDeHabilitacao carteiraDeHabilitacao)
            : base(nome, email, dataNascimento, cpf)
        {
            CarteiraDeHabilitacao = carteiraDeHabilitacao;
            Avaliacoes = new List<Avaliacao>();
        }

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

        public void AdicionarCorrida(Corrida corrida)
        {
            Corridas.Add(corrida);
        }

        public void RemoverCorrida(Corrida corrida)
        {
            Corridas.Remove(corrida);
        }

        public Corrida? ObterCorrida(Guid id)
            => Corridas.FirstOrDefault(v => v.CorridaID == id);


        public void AlterarCorrida(Corrida corrida)
        {
            RemoverCorrida(ObterCorrida(corrida.CorridaID));
            AdicionarCorrida(corrida);
        }
    }

    public class CarteiraDeHabilitacao
    {
        public string Numero { get; set; }

        [EnumDataType(typeof(Categoria), ErrorMessage = "O campo Categoria deve ser um valor válido.")]
        public Categoria Categoria { get; set; }
        public DateTime DataVencimento { get; set; }

        public CarteiraDeHabilitacao(string numero, Categoria categoria, DateTime dataVencimento)
        {
            Numero = numero;
            Categoria = categoria;
            DataVencimento = dataVencimento;
        }
    }

}
