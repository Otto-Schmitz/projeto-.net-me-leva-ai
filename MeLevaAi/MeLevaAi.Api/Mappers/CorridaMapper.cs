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
                TempoEstimando = corrida.TempoEstimado
            };
    }

}
