using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domain;

namespace MeLevaAi.Api.Mappers
{
    public static class VeiculoMapper
    {
        public static Veiculo ToVeiculo(this AdicionarVeiculoRequest request)
            => new(request.MotoristaId, request.Placa, request.Marca, request.Modelo, request.Ano, request.Cor, request.FotoUrl, request.QuantidadeDeLugares, request.Categoria);

        public static Veiculo ToAlterarVeiculo(this AlterarVeiculoRequest request)
            => new(request.MotoristaId, request.Placa, request.Marca, request.Modelo, request.Ano, request.Cor, request.FotoUrl, request.QuantidadeDeLugares, request.Categoria);

        public static VeiculoDto ToVeiculoDto(this Veiculo veiculo)
        {
            return new VeiculoDto
            {
                Id = veiculo.Id,
                MotoristaId = veiculo.MotoristaId,
                Placa = veiculo.Placa,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Cor = veiculo.Cor,
                FotoUrl = veiculo.FotoUrl,
                QuantidadeDeLugares = veiculo.QuantidadeDeLugares,
                Categoria = veiculo.Categoria
            };
        }
    }
}
