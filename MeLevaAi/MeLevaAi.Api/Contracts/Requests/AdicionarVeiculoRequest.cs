using MeLevaAi.Api.Domains;
using System;
using System.ComponentModel.DataAnnotations;

namespace MeLevaAi.Api.Contracts.Requests
{
    public class AdicionarVeiculoRequest
    {
        [Required(ErrorMessage = "O campo Id do Motorista é obrigatório.")]
        public Guid MotoristaId { get; set; }

        [Required(ErrorMessage = "O campo Placa do Veículo é obrigatório.")]
        [MaxLength(7, ErrorMessage = "O campo Placa do Veículo deve ter no máximo 7 caracteres.")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O campo Marca do Veículo é obrigatório.")]
        [MaxLength(32, ErrorMessage = "O campo Marca do Veículo deve ter no máximo 32 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo Modelo do Veículo é obrigatório.")]
        [MaxLength(32, ErrorMessage = "O campo Modelo do Veículo deve ter no máximo 32 caracteres.")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo Ano do veículo é obrigatório.")]
        [Range(1900, 2100, ErrorMessage = "O campo Ano do veículo deve estar entre 1900 e 2100.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O campo Cor do veículo é obrigatório.")]
        [MaxLength(32, ErrorMessage = "O campo Cor do Veículo deve ter no máximo 32 caracteres.")]
        public string Cor { get; set; }

        public string FotoUrl { get; set; }

        [Required(ErrorMessage = "O campo quantidade de lugares do veículo é obrigatório.")]
        public int QuantidadeDeLugares { get; set; }

        [EnumDataType(typeof(Categoria), ErrorMessage = "O campo Categoria do Veículo deve ser um valor válido.")]
        public Categoria Categoria { get; set; }
    }
}
