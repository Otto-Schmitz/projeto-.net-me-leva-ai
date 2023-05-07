using MeLevaAi.Api.Domain;

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

        public Corrida(Guid passageiroId, Veiculo veiculo, Coordenadas pontoInicial, Coordenadas pontoFinal)
        {
            PassageiroId = passageiroId;
            Veiculo = veiculo;
            PontoInicial = pontoInicial;
            PontoFinal = pontoFinal;
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
