using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class FinalizarCorridaDto : Notifiable
    {
        public Guid CorridaId { get; set; }

        public double Valor { get; set; }

    }
}
