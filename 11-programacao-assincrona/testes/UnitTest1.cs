using Xunit;
using System;
using Project;
namespace testes;

public class UnitTest1
{
    [Fact]
    public async Task BaixarDados_DeveRetornarDadosNaoVazio()
    {
        // Act
        string resultado = await Program.BaixarDados("https://jsonplaceholder.typicode.com/posts/1");
    
        // Assert
        Assert.NotEmpty(resultado);
    
    }

    [Fact]
    public async Task BaixarDados_ComURLInvalida_DeveLancarExcecao()
    {
        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(
            () => Program.BaixarDados("https://url-que-nao-existe-de-verdade-12345.com")
        );
    }
}
