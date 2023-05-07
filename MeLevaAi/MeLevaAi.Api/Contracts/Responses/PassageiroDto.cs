using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class PassageiroDto : Notifiable
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Cpf { get; set; }

        public double Saldo { get; set; }

        public List<Avaliacao> Avaliacoes { get; set; }

        public bool EmCorrida { get; set; }
    }
}
