namespace MeLevaAi.Api.Domain
{
    public class Veiculo
    {
        public Veiculo(Guid motoristaId, string placa, string marca, string modelo, int ano, string cor, string fotoUrl, int quantidadeDeLugares, Categoria categoria)
        {
            MotoristaId = motoristaId;
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Ano = ano;
            Cor = cor;
            FotoUrl = fotoUrl;
            QuantidadeDeLugares = quantidadeDeLugares;
            Categoria = categoria;
        }

        public Guid Id { get; init; } = Guid.NewGuid();

        public Guid MotoristaId { get; private set; }

        public string Placa { get; private set; }

        public string Marca { get; private set; }

        public string Modelo { get; private set; }

        public int Ano { get; private set; }

        public string Cor { get; private set; }

        public string FotoUrl { get; private set; }

        public int QuantidadeDeLugares { get; private set; }

        public Categoria Categoria { get; private set; }

        public DateTime DataCriacao { get; init; } = DateTime.Now;

        public void Alterar(Veiculo veiculo)
        {
            MotoristaId = veiculo.MotoristaId;
            Placa = veiculo.Placa;
            Marca = veiculo.Marca;
            Modelo = veiculo.Modelo;
            Ano = veiculo.Ano;
            Cor = veiculo.Cor;
            FotoUrl = veiculo.FotoUrl;
            QuantidadeDeLugares = veiculo.QuantidadeDeLugares;
            Categoria = veiculo.Categoria;
        }
    }
}