using project;
using Xunit;
namespace _07program.Tests;

public class UnitTest1
{
  [Fact]
public void Login_NaoDeveLancarExcecao()
{
    // Arrange
    string usuario = "pessoa1";
    string senha = "123456";
    Metodo pessoa1 = new Metodo("pessoa1", "123456");

    // Act
    var excecao = Record.Exception(() => pessoa1.Login(usuario, senha));

    // Assert
    Assert.Null(excecao);
}
   [Fact]
   public void Login_DeveLancarException()
   {
    // Arrange
    string usuario = "pssoa1";
    string senha = "1234";
    Metodo pessoa1 = new Metodo("pessoa1", "123456");
   
    // Act
    Action verificacao = () => pessoa1.Login(usuario, senha);
   
    // Assert
    Assert.Throws<CredenciaisInvalidasException>(verificacao);

   }
}
