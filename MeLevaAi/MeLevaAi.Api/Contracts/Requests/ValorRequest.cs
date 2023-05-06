using MeLevaAi.Api.Domain;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class ValorRequest
    {
        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        public double Valor { get; set; }
    }
}
