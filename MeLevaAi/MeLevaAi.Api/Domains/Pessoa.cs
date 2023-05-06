using System.Text.RegularExpressions;
using CpfLibrary;

namespace MeLevaAi.Api.Domains
{
    public abstract class Pessoa
    {
        protected Pessoa(string nome, string email, DateTime dataNascimento, string cpf)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            // fazer exception
            if (VerificaCpf(cpf))
                Cpf = cpf;
        }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Cpf { get; set; }

        public abstract bool VerificaIdadeMinima();

        public bool VerificaCpf(string cpf)
        {//regex nao funciona
            return CpfLibrary.Cpf.Check(cpf);
        }
    }
}
