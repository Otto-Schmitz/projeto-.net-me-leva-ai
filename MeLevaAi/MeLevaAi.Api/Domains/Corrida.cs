using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Domains
{
    public class Corrida
    {
        public Guid CorridaID { get; init; } = Guid.NewGuid();

        public Guid PassageiroId { get; private set; }

        public Veiculo Veiculo { get; private set; }

        public Coordenadas PontoInicial { get; private set; }

        public Coordenadas PontoFinal { get; private set; }

        public Avaliacao AvaliacaoDoMotorista { get; private set; }

        public Avaliacao AvaliacaoDoPassageiro { get; private set; }

        public int TempoEstimado { get; init; } = new Random().Next(5, 10);

        public double ValorEstimado { get; private set; }

        public StatusCorrida StatusCorrida { get; set; } = StatusCorrida.Solicitada;

        public Corrida(Guid passageiroId, Veiculo veiculo, Coordenadas pontoInicial, Coordenadas pontoFinal)
        {
            PassageiroId = passageiroId;
            Veiculo = veiculo;
            PontoInicial = pontoInicial;
            PontoFinal = pontoFinal;
        }

        public void AtualizarValorEstimado(double valor)
        {
            ValorEstimado = valor;
        }
    }

    public class Coordenadas
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Coordenadas(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
