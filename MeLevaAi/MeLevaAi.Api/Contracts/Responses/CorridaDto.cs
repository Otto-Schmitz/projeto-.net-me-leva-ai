using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Domains;
using MeLevaAi.Api.Validations;

namespace MeLevaAi.Api.Contracts.Responses
{
    public class CorridaDto : Notifiable
    {
        public Guid Id { get; set; }

        public string NomePassageiro { get; set; }

        public string NomeMotorista { get; set; }

        public Veiculo Veiculo { get; set; }

        public int TempoEstimando{ get; set; }

        public Coordenadas PontoInicial { get; set; }

        public Coordenadas PontoFinal { get; set; }
    }
}
