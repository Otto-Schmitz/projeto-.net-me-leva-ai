using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class ChamarCorridaDto : Notifiable
    {
        public Guid CorridaID { get; set; }

        public Veiculo Veiculo { get; set; }

        public int TempoEstimado { get; set; }
    }
}