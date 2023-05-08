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
