using MeLevaAi.Api.Contracts.Requests;
using MeLevaAi.Api.Contracts.Responses;
using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Mappers
{
    public static class PassageiroMapper
    {
        public static Passageiro ToPassageiro(this AdicionarPassageiroRequest request)
        
            => new(request.Nome, request.Email, request.DataNascimento, request.Cpf);
        

        public static PassageiroDto ToPassageiroDto(this Passageiro passageiro)
            => new()
            {
                Id = passageiro.Id,
                Nome = passageiro.Nome,
                Email = passageiro.Email,
                DataNascimento = passageiro.DataNascimento,
                Cpf = passageiro.Cpf,
                Saldo = passageiro.Saldo,
            };
    }
}
