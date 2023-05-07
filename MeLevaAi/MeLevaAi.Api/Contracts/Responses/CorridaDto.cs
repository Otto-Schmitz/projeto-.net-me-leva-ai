using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class CorridaDto
    {
        public Guid Id { get; set; }

        public string NomePassageiro { get; set; }

        public string NomeMotorista { get; set; }

        public Veiculo Veiculo { get; set; }

        public int TempoEstimando{ get; set; }
    }
}
