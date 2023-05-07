namespace MeLevaAi.Api.Domains
{
    public class Avaliacao
    {
        public Guid PessoaId { get; private set; }

        public Guid CorridaId { get; private set; }

        public int Nota { get; private set; }

        public string Descricao { get; private set; }
    }
}
