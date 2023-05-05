namespace MeLevaAi.Api.Domains
{
    public abstract class Pessoa
    {
        protected Pessoa(string nome, string email, DateOnly dataNascimento, string cpf)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Cpf = cpf;
        }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateOnly DataNascimento { get; set; }

        public string Cpf { get; set; }

        public abstract bool VerificaIdadeMinima();
    }
}
