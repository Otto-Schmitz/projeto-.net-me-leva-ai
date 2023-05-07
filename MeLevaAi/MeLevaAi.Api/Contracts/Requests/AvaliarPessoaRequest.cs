using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Domains;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class AvaliarPessoaRequest
    {
        [Required(ErrorMessage = "O campo PontoInicial é obrigatório.")]
        [Range(1, 5, ErrorMessage = "A nota deve estar entre 1 e 5.")]
        public int Nota { get; set; }

        [Required(ErrorMessage = "O campo PontoFinal é obrigatório.")]
        public string Descricao { get; set; }
    }
}
