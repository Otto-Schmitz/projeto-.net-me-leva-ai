using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class AdicionarPassageiroRequest
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve ter apenas 10 caracteres.")]
        public string Nome { get; set; }
    }
}
