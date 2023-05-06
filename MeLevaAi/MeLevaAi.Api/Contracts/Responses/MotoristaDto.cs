using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class MotoristaDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateOnly DataNascimento { get; set; }

        public string Cpf { get; set; }

        public Categoria Categoria { get; set; }

    }
}
