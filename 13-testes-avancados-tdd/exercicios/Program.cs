// Exercício 1

using System.ComponentModel;
using Xunit;

namespace Project;
// public class Metodos
// {
//     public static double Somar(double a, double b)
//     {
//         return a + b;
//     }

//     public static double Subtrair(double a, double b)
//     {
//         return a - b;
//     }

//     public static double Dividir(double a, double b)
//     {
//        if (b == 0)
//         {
//             throw new DivideByZeroException();
//         }
//         return a / b;
//     }

//     public static double Multiplicacao(double a, double b)
//     {
//         return a * b;
//     }

    
// }

// public class Program
// {
//     public static void Main()
//     {
       

//     }
// }

// Exercício 2

// public interface IEmailService
// {
//     public void EnviarEmail(string destinatario, string mensagem);
// }

// public class PedidoService
// {
//     private readonly IEmailService emailService;

//     public PedidoService(IEmailService emailService)
//     {
//         this.emailService = emailService;
//     }

//    public void CriarPedido(string cliente, decimal valor)
//     {
//         Console.WriteLine($"Pedido criado para {cliente}");

//         emailService.EnviarEmail(cliente, "Mensagem enviada");
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
        
//     }
// }

// Exercício 3

// public class ValidadorEmail
// {
//     public bool Validar(string Email)
//     {
//        return Email.Contains("@");
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
        
//     }
// }

// Exercício 4

// public class ValidadorEmailFixture : IDisposable
// {
//     public ValidadorEmail validador { get; set; }
//     public ValidadorEmailFixture()
//     {
//         validador = new ValidadorEmail();
//     }

//     public void Dispose() { }
// }

// public class ValidadorEmailFixtureTestes : IClassFixture<ValidadorEmailFixture>
// {
//     private readonly ValidadorEmailFixture fixture;

//     public ValidadorEmailFixtureTestes(ValidadorEmailFixture fixture)
//     {
//         this.fixture = fixture;
//     }
// }

// TESTE

public class Metodo
{
    public static bool ValidadorSenha(string senha)
    {
        if (senha.Length < 8) return false;
        if (!senha.Any(char.IsUpper)) return false;
        if (!senha.Any(char.IsDigit)) return false;

        return true;
    }

    public static void Main()
    {
        
    }
}

