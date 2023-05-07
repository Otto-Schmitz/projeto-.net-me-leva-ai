using MeLevaAi.Api.Domain;

public class CarteiraDeHabilitacao
{
    public string Numero { get; set; }
    public Categoria Categoria { get; set; }
    public DateTime DataVencimento { get; set; }

    public CarteiraDeHabilitacao(string numero, Categoria categoria, DateTime dataVencimento)
    {
        Numero = numero;
        Categoria = categoria;
        DataVencimento = dataVencimento;
    }
}
