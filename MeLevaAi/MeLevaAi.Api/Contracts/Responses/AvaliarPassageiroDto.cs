using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class AvaliarPassageiroDto : Notifiable
    {
        public Guid CorridaId { get; set; }

        public string NomePassageiro { get; set; }

        public int Nota { get; set; }

        public string Descricao { get; set; }

    }
}
