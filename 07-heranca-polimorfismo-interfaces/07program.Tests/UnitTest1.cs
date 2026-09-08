using Xunit;
using project;
namespace _07program.Tests;

public class UnitTest1
{
    [Fact]
    public void CalcularSalario_Gerente_DeveRetornarSalarioBaseMaisBonusFixo()
    {
        // Arrange
        string name = "Pedro";
        decimal salarioBase = 2000m;
        decimal bonusFixo = 1000m;
        decimal salarioEsperado = 3000m;

        Gerente gerente = new Gerente(name, salarioBase, bonusFixo);
    
        // Act
        decimal salarioCaculado = gerente.CalcularSalario();

        // Assert
        Assert.Equal(salarioEsperado, salarioCaculado);
    }

    [Fact]
    public void CalcularSalario_Vendedor_DeveRetornarSalariobaseMaisComissao()
    {
        // Arrange
        string name = "João";
        decimal salarioBase = 3000m;
        decimal totalVendas = 10000m;
        decimal porcentualComissao = 5m;
        decimal salarioEsperado = 3500m;

        Vendedor vendedor = new Vendedor(name, salarioBase, totalVendas, porcentualComissao);
    
        // Act
        decimal salarioCalculado = vendedor.CalcularSalario();
    
        // Assert
        Assert.Equal(salarioEsperado, salarioCalculado);
    }
}
