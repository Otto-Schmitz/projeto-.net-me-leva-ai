using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class IniciarCorridaDto : Notifiable
    {
        public double TempoEstimadoDestino { get; set; }

        public double ValorEstimado { get; set; }
    }
}

