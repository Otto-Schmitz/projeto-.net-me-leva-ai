using MeLevaAi.Api.Domain;
using System.ComponentModel.DataAnnotations;

public class CarteiraDeHabilitacao
{
    public string Numero { get; set; }

    [EnumDataType(typeof(Categoria), ErrorMessage = "O campo Categoria deve ser um valor válido.")]
    public Categoria Categoria { get; set; }
    public DateTime DataVencimento { get; set; }

    public CarteiraDeHabilitacao(string numero, Categoria categoria, DateTime dataVencimento)
    {
        Numero = numero;
        Categoria = categoria;
        DataVencimento = dataVencimento;
    }
}
