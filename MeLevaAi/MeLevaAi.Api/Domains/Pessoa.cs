using System.Text.RegularExpressions;

namespace MeLevaAi.Api.Domains
{
    public abstract partial class Pessoa
    {
        protected Pessoa(string nome, string email, DateOnly dataNascimento, string cpf)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            // fazer exception
            if (VerificaCpf())
                Cpf = cpf;
        }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateOnly DataNascimento { get; set; }

        public string Cpf { get; set; }

        [GeneratedRegex("([0 - 9]{ 2}[.]?[0 - 9]{ 3[.]?[0 - 9]{ 3}[/]?[0 - 9]{ 4}[-]?[0 - 9]{ 2})| ([0 - 9]{ 3}[.]?[0 - 9]{ 3}[.]?[0 - 9]{ 3}[-]?[0 - 9]{ 2})")]
        public partial Regex CpfRegex();

        public abstract bool VerificaIdadeMinima();

        public bool VerificaCpf()
        {
            return CpfRegex()
                       .IsMatch(Cpf);
        }
    }
}
