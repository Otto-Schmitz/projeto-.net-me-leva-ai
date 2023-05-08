using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class FinalizarCorridaRequest
    {
        [Required(ErrorMessage = "O campo CorridaId é obrigatório.")]
        public Guid? CorridaId { get; set; }
    }
}
