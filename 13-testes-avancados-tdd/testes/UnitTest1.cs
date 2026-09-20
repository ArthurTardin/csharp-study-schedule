using Xunit;
using Project;
using Moq;
using System.ComponentModel.DataAnnotations;
namespace testes;

public class UnitTest1
{

        //Exercício 1
    //    [Fact]
    //    public void Somar_DeveRetornarErro_NaoExisteFuncao()
    //    {
    //         double a = 2;
    //         double b = 3;

    //         double resultado = Metodos.Somar(a, b);

    //         Assert.Equal(5, resultado);
    //    }

    //    [Fact]
    //    public void Subtrair_DeveRetornarTrue_Green()
    //    {
    //         double a = 5;
    //         double b = 2;

    //         double resultado = Metodos.Subtrair(a, b);

    //         Assert.Equal(3, resultado);
    //    }

    //    [Fact]
    //    public void Dividir_DeveRetornarExcecao_DivisaoPorZero()
    //    {
    //         Assert.Throws<DivideByZeroException>(() => Metodos.Dividir(10, 0));
    //    }

        // Exercício 2

        // [Fact]
        // public void CriarPedido_DeveEnviarEmail()
        // {
        //    var mockEmail = new Mock<IEmailService>();
        //    var service = new PedidoService(mockEmail.Object);

        //    service.CriarPedido("Pablo@example.com", 100);

        //    mockEmail.Verify(m => m.EnviarEmail("Pablo@example.com", "Mensagem enviada"), Times.Once());
        // }

        // Exercício 3

        // [Theory]
        // [InlineData("arthur@example.com", true)]
        // [InlineData("pablo@example.com", true)]
        // [InlineData("Carlos@example.com", true)]
        // [InlineData("Ricardo@example.com", true)]
        // [InlineData("Tathiana@example.com", true)]
        // [InlineData("pabloexample.com", false)]
        // [InlineData("Antonioexample.com", false)]
        // [InlineData("Pedroexample.com", false)]
        // [InlineData("Joao", false)]
        // [InlineData("Agatha", false)]
        // public void ValidadorEmail_ComEmailInvalido_RetornaFalse(string email, bool esperado)
        // {
        //     var resultado = ValidadorEmail.Validar(email);

        //     Assert.Equal(esperado, resultado);
        // }

        // Exercício 4

    // public class ValidadorEmailFixtureTestes : IClassFixture<ValidadorEmailFixture>
    // {
    //     private readonly ValidadorEmailFixture fixture;

    //     public ValidadorEmailFixtureTestes(ValidadorEmailFixture fixture)
    //     {
    //         this.fixture = fixture;
    //     }

    //     [Fact]
    //     public void ValidadorEmail_Correto_retornaTrue()
    //     {
    //         var resultado = fixture.validador.Validar("arthur@example.com");
    //         Assert.True(resultado);
    //     }

    //     [Fact]
    //     public void ValidadorEmail_Incorreto_retornaFalse()
    //     {
    //         var resultado = fixture.validador.Validar("arthur");
    //         Assert.False(resultado);
    //     }
    // } 

    // DEBUG

//    [Fact]
//    public void TestName()
//    {
//         var mockPagamento = new Mock<IPagamentoService>();
// mockPagamento
//     .Setup(m => m.Processar(It.IsAny<decimal>()))
//     .Returns(true);

// var service = new PedidoService(mockPagamento.Object);
// service.CriarPedido("Arthur", 100);

// mockPagamento.Verify(m => m.Processar(It.IsAny<decimal>()), Times.Once());
//    }
    // Teste

    [Theory]
    [InlineData("12345678", false)]
    [InlineData("A1", false)]
    [InlineData("asrd242134234", false)]
    [InlineData("123", false)]
    [InlineData("12AAAAa", false)]
    [InlineData("Arthur123", true)]
    [InlineData("Pablo456", true)]
    [InlineData("Ricardo78910", true)]
    [InlineData("12345678AAA", true)]
    [InlineData("12345Sete", true)]
    public void ValidadorSenha_VerficiarHipoteses(string senha, bool esperado)
    {
        var resultado = Metodo.ValidadorSenha(senha);

        Assert.Equal(esperado, resultado);
    }

}