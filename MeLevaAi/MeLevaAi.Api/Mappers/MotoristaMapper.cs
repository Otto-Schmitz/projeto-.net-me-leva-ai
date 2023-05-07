using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Mappers
{
    public static class MotoristaMapper
    {
        public static Motorista ToMotorista(this AdicionarMotoristaRequest request)
            => new(request.Nome, request.Email, request.DataNascimento, request.Cpf, request.CarteiraDeHabilitacao);

        public static MotoristaDto ToMotoristaDto(this Motorista motorista)
            => new()
            {
                Id = motorista.Id,
                Nome = motorista.Nome,
                Email = motorista.Email,
                DataNascimento = motorista.DataNascimento,
                Cpf = motorista.Cpf,
                CarteiraDeHabilitacao = motorista.CarteiraDeHabilitacao,
                Saldo = motorista.Saldo,
            };
        
    }
}
