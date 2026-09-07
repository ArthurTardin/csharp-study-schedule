using System;
using Xunit;
using project;

namespace Program.Tests;

public class ContaBancariaTests
{
    [Fact]
    public void Depositar_DeveAumentarSaldoCorretamente()
    {
        // Arrange
        var conta = new ContaBancaria(100m);
        decimal valorDeposito = 50m;

        // Act
        conta.Depositar(valorDeposito);

        // Assert
        Assert.Equal(150m, conta.Saldo);
    }

    [Fact]
    public void Sacar_ComValorValido_DeveDiminuirSaldoCorretamente()
    {
        // Arrange
        var conta = new ContaBancaria(100m);
        decimal valorSaque = 40m;

        // Act
        conta.Sacar(valorSaque);

        // Assert
        Assert.Equal(60m, conta.Saldo);
    }

    [Fact]
    public void Sacar_ComValorMaiorQueSaldo_DeveLancarExcecaoENaoAlterarSaldo()
    {
        // Arrange
        var conta = new ContaBancaria(100m);
        decimal valorSaqueInvalido = 150m;

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => conta.Sacar(valorSaqueInvalido));
        Assert.Equal(100m, conta.Saldo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Depositar_ComValorInvalido_DeveLancarExcecao(decimal valorInvalido)
    {
        // Arrange
        var conta = new ContaBancaria(100m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => conta.Depositar(valorInvalido));
        Assert.Equal(100m, conta.Saldo);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void Sacar_ComValorInvalido_DeveLancarExcecao(decimal valorInvalido)
    {
        // Arrange
        var conta = new ContaBancaria(100m);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => conta.Sacar(valorInvalido));
        Assert.Equal(100m, conta.Saldo);
    }

    [Fact]
    public void CriarConta_ComSaldoInicialNegativo_DeveLancarExcecao()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ContaBancaria(-10m));
    }
}