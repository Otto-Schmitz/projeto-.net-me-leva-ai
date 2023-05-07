using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Helpers
{
    public class CalculadorDistancia
    {
        public static double CalcularDistancia(Coordenadas pontoA, Coordenadas pontoB)
        {
            return Math.Sqrt(Math.Pow(pontoB.X - pontoA.X, 2) + Math.Pow(pontoB.Y - pontoA.Y, 2));
        }
    }
}
