using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Domains;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class ChamarCorridaRequest
    {
        [Required(ErrorMessage = "O campo PassageiroId é obrigatório.")]
        public Guid? PassageiroId { get; set; }

        [Required(ErrorMessage = "O campo PontoInicial é obrigatório.")]
        public Coordenadas PontoInicial { get; set; }

        [Required(ErrorMessage = "O campo PontoFinal é obrigatório.")]
        public Coordenadas PontoFinal { get; set; }
    }
}


//public Guid CorridaID { get; init; } = Guid.NewGuid();

//public Guid PassageiroId { get; private set; }

//public Veiculo Veiculo { get; private set; }

//public Coordenadas PontoInicial { get; private set; }

//public Coordenadas PontoFinal { get; private set; }

//public Avaliacao AvaliacaoDoMotorista { get; private set; }

//public Avaliacao AvaliacaoDoPassageiro { get; private set; }