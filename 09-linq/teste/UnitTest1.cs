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

    [Fact]
    public void NomesDosProdutosCaros_ListaNaoSatisfazCondicao_DeveRetornarListVazia()
    {
        // Arrange
        List<Produto> produtos = new List<Produto>();

        Produto produto1 = new Produto("Produto1", 2, 100);
        Produto produto2 = new Produto("Produto2", 3, 500);
        Produto produto3 = new Produto("Produto3", 8, 300);
        Produto produto4 = new Produto("Produto4", 8, 700);
        Produto produto5 = new Produto("Produto5", 1, 900);

        produtos.Add(produto1);
        produtos.Add(produto2);
        produtos.Add(produto3);
        produtos.Add(produto4);
        produtos.Add(produto5);
    
        // Act
        List<string> resultado = FiltroProdutos.NomesDosProdutosCaros(produtos, 1000);
    
        // Assert
        Assert.Empty(resultado);
    }

    [Fact]
    public void NomesDosProdutosCaros_ListaComMultiplosCriterios_DeveRetornarNomesCertos()
    {
        // Arrange
         List<Produto> produtos = new List<Produto>();
         
        Produto produto1 = new Produto("Produto1", 2, 1000);
        Produto produto2 = new Produto("Produto2", 3, 999);
        Produto produto3 = new Produto("Produto3", 8, 1500);
        Produto produto4 = new Produto("Produto4", 8, 3000);
        Produto produto5 = new Produto("Produto5", 1, 300);

         produtos.Add(produto1);
        produtos.Add(produto2);
        produtos.Add(produto3);
        produtos.Add(produto4);
        produtos.Add(produto5);
    
        // Act
        List<string> resultado = FiltroProdutos.NomesDosProdutosCaros(produtos, 1000);
    
        // Assert
        List<string> esperado = new List<string> { "Produto1", "Produto3", "Produto4" };
        Assert.Equal(esperado, resultado);
    }
}
