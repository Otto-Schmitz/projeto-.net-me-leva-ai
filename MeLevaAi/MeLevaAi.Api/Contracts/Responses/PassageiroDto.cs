﻿namespace MeLevaAi.Api.Contracts.Responses
{
    public class PassageiroDto
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public DateOnly DataNascimento { get; set; }

        public string Cpf { get; set; }
    }
}
