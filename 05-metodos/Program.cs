// // exercício 1:

// using System.Reflection.Metadata.Ecma335;

// static bool VerificadorCPF(string cpf)
// {
//     if (string.IsNullOrWhiteSpace(cpf))
//     {
//         return false;
//     }
//     if (cpf.Length != 11)
//     {
//         return false;
//     }

//     foreach (var letter in cpf)
//     {
//         if (!char.IsDigit(letter))
//         {
//             return false;
//         }
//     }
//     return true;
// }

// Console.WriteLine(VerificadorCPF("50222222222"));

// // Exercício 2

// static bool EhPar(int a)
// {
//     if (a % 2 == 0)
//     {
//         return true;
//     }
//     return false;
// }

// Console.WriteLine(EhPar(3)); // false
// Console.WriteLine(EhPar(4)); // true
// Console.WriteLine(EhPar(9)); // false


// static bool EhPrimo(int a)
// {
//     if (a <= 0) return false;
//     if (a == 2) return true;
    
//     for (int b = 2; b < a; b++)
//     {
//         if (a % b == 0)
//         {
//             return false;
//         }
//     }
//     return true;
// }

// Console.WriteLine(EhPrimo(9)); // false
// Console.WriteLine(EhPrimo(2)); // true
// Console.WriteLine(EhPrimo(-13)); // false
// Console.WriteLine(EhPrimo(13)); // true

// static int MDC (int a, int b)
// {
//     while (b != 0)
//     {
//         int resto = a % b;
//         a = b;
//         b = resto;
//     }

//     return Math.Abs(a);
// }

// Console.WriteLine(MDC(25, 50)); // 25

// // Exercício 3

// static int Fatorial (int n)
// {
//     if (n <= 1) return 1;
//     return n * Fatorial(n - 1);
// }

// // Exercício 4

// static int Fibonacci(int n)
// {
//     if (n <= 0) return 0;
//     if (n == 1) return 1;

//     return Fibonacci(n - 1) +  Fibonacci(n - 2);
// }

// Console.WriteLine(Fibonacci(20));

// // Exercício 5

// class Programd
// {
//     static int Maior(int a, int b) => Math.Max(a, b);
// static double Maior(double a, double b) => Math.Max(a, b);
// static int Maior(int a, int b, int c) => Math.Max(a, Math.Max(b, c));

//     static void Main()
//     {
//         Console.WriteLine(Maior(5, 3)); // 5
//         Console.WriteLine(Maior(10.5, 13.67)); // 13,67
//         Console.WriteLine(Maior(90, 40, 100)); //100
//     }
// }

// DEBUG

//   static int Somatorio(int n)
// {
//     if (n <= 1) return 1;
//     return n + Somatorio(n - 1);
// }

// static void Trocar(ref int a, ref int b)
// {
//     int temp = a;
//     a = b;
//     b = temp;
// }

// int x = 5;
// int y = 10;
// Trocar(ref x, ref y);
// Console.WriteLine($"x = {x}, y = {y}"); // deveria imprimir x = 10, y = 5