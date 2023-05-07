using MeLevaAi.Api.Domains;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class AlterarMotoristaRequest
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Nome deve ter apenas 10 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O campo Email deve ter apenas 10 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Data Nascimento é obrigatório.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo Cpf é obrigatório.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Categoria do motorista é obrigatório.")]
        public Categoria Categoria { get; set; }
    }
}
