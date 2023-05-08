using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class AvaliarMotoristaDto : Notifiable
    {
        public Guid CorridaId { get; set; }

        public string NomeMotorista { get; set; }

        public int Nota { get; set; }

        public string Descricao { get; set; }

    }
}
