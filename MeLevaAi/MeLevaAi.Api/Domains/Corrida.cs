using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Domains
{
    public class Corrida
    {
        public Guid CorridaId { get; init; } = Guid.NewGuid();

        public Guid PassageiroId { get; private set; }

        public Veiculo Veiculo { get; private set; }

        public Coordenadas PontoInicial { get; private set; }

        public Coordenadas PontoFinal { get; private set; }

        public Avaliacao AvaliacaoDoMotorista { get; private set; }

        public Avaliacao AvaliacaoDoPassageiro { get; private set; }

        public int TempoEstimadoChegada { get; init; } = new Random().Next(5, 10);

        public double TempoEstimadoDestino { get; private set; }

        public double ValorEstimado { get; private set; }

        public double ValorFinal { get; private set; }

        public StatusCorrida StatusCorrida { get; private set; } = StatusCorrida.Solicitada;

        public DateTime TempoInicial { get; private set; }

        public string TempoFinal { get; private set; }

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

        public void AtualizarTempoEstimadoDestino(double tempo)
        {
            TempoEstimadoDestino = tempo;
        }

        public void AtualizarStatusCorrida(StatusCorrida statusCorrida)
        {
            StatusCorrida = statusCorrida;
        }

        public void AdicionarTempoInicial(DateTime tempoInicial)
        {
            TempoInicial = tempoInicial;
        }

        public void AdicionarTempoFinal(string tempoFinal)
        {
            TempoFinal = tempoFinal;
        }

        public void AdicionarValorFinal(double valorFinal)
        {
            ValorFinal = valorFinal;
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
