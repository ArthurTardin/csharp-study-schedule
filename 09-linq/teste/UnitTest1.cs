using Xunit;
using Project;

public class UnitTest1
{
        [Fact]
    public void NomesDosProdutosCaros_ComListaVazia_DeveRetornarListaVazia()
    {
        // Arrange
        List<Produto> produtos = new List<Produto>();

        // Act
        List<string> resultado = FiltroProdutos.NomesDosProdutosCaros(produtos, 1000);

        // Assert
        Assert.Empty(resultado); // Assert.Empty confirma que a coleção não tem nenhum item
    }
}
