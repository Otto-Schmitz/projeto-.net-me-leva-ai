using MeLevaAi.Api.Domain;
using MeLevaAi.Api.Domains;

namespace MeLevaAi.Api.Validations
{
    public static class VeiculoValidation
    {
        public static bool VerificarCategoria(Veiculo veiculo, Motorista motorista)
        {
            return veiculo.Categoria == motorista.CarteiraDeHabilitacao.Categoria;
        }
    }
}
