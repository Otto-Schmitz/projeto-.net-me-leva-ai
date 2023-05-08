using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Mappers
{
    public static class CorridaMapper
    {
        public static Corrida ToCorrida(this ChamarCorridaRequest request, Passageiro passageiro, Veiculo veiculo)
            => new(passageiro.Id, veiculo, request.PontoInicial, request.PontoFinal);

        public static CorridaDto ToCorridaDto(this Corrida corrida, Passageiro passageiro, Motorista motorista)
            => new()
            {
                Id = corrida.CorridaID,
                NomePassageiro = passageiro.Nome,
                NomeMotorista = motorista.Nome,
                Veiculo = corrida.Veiculo,
                TempoEstimando = corrida.TempoEstimadoChegada
            };

        public static ChamarCorridaDto ToChamarCorridaDto(this Corrida corrida)
            => new()
            {
                CorridaID = corrida.CorridaID,
                Veiculo = corrida.Veiculo,
                TempoEstimado = corrida.TempoEstimadoChegada,
            };

        public static IniciarCorridaDto ToIniciarCorridaDto(this Corrida corrida)
            => new()
            {
                TempoEstimadoDestino = corrida.TempoEstimadoDestino,
                ValorEstimado = corrida.ValorEstimado,
            };

        public static AvaliarMotoristaDto ToAvaliarMotoristaDto(this Corrida corrida, Motorista motorista)
            => new()
            {
                CorridaId = corrida.CorridaID,
                NomeMotorista = motorista.Nome,
                Nota = corrida.AvaliacaoDoMotorista.Nota,
                Descricao = corrida.AvaliacaoDoMotorista.Descricao,
            };

        public static AvaliarPassageiroDto ToAvaliarPassageiroDto(this Corrida corrida, Passageiro passageiro)
            => new()
            {
                CorridaId = corrida.CorridaID,
                NomePassageiro = passageiro.Nome,
                Nota = corrida.AvaliacaoDoPassageiro.Nota,
                Descricao = corrida.AvaliacaoDoPassageiro.Descricao,
            };
    }

}
