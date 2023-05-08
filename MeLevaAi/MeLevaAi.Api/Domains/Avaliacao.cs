namespace MeLevaAi.Api.Domains
{
    public class Avaliacao
    {
        public Guid PessoaId { get; set; }

        public Guid CorridaId { get; set; }

        public int Nota { get; set; }

        public string Descricao { get; set; }

        public Avaliacao(Guid pessoaId, Guid corridaId, int nota, string descricao)
        {
            PessoaId = pessoaId;
            CorridaId = corridaId;
            Nota = nota;
            Descricao = descricao;
        }
    }
}
