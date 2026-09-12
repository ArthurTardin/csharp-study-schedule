// Exercício 1

// public class Email
// {
//     public static List<Email> cadastros = new List<Email>();
//     public string _Email { get; set; }
//     public string PassWord { get; set; }

//     public Email(string email, string passWord)
//     {
//         this._Email = email;
//         this.PassWord = passWord;
//     }

//     public static bool Cadastrar(string email, string passWord)
//     {
//         if (cadastros.Any(e => e._Email == email))
//             return false;

//         cadastros.Add(new Email(email, passWord));
//         return true;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Console.Write("Escreva o Email: ");
//         string ? email = Console.ReadLine();

//         Console.Write("Escreva a senha: ");
//         string ? PassWord = Console.ReadLine();

//         if (string.IsNullOrWhiteSpace(email))
//         {
//             Console.WriteLine("Email inválido!");
//         }
//         else if (string.IsNullOrWhiteSpace(PassWord))
//         {
//             Console.WriteLine("Senha inválido!");
//         }
//         else
//         {
//              if (Email.Cadastrar(email, PassWord) == true)
//             {
//                 Console.WriteLine("Cadastrado com sucesso!");
//             }
//             else
//             {
//                 Console.WriteLine("Erro: email já existe!");
//             }
//         }
//     }
// }

// Exercício 2

// public class Program
// {
//     delegate int Operacao(int a, int b);

//     static int Somar(int a, int b) => a + b;
//     static int Subtrair(int a, int b) => a - b;
//     static int Multiplicar(int a, int b) => a * b;

//     public static void Main()
//     {
//         int a, b, choice;
//         Operacao op;
//         Console.Write("Digite o valor 1: ");
//         if (!int.TryParse(Console.ReadLine(), out a))
//         {
//             Console.WriteLine("Valor de a inválido"); 
//             return;
//         }

//         Console.Write("Digite o valor 2: ");
//         if (!int.TryParse(Console.ReadLine(), out b))
//         {
//             Console.WriteLine("Valor de b inválido"); 
//             return;
//         }

//         Console.WriteLine("Escolha a sua operação: \n 1 - Soma \n 2 - Subtração \n 3 - Multiplicação");
//         int.TryParse(Console.ReadLine(), out choice);

//         switch (choice)
//         {
//             case 1:
//                 op = Somar;
//                 Console.WriteLine(op(a, b));
//                 break;
            
//             case 2:
//                 op = Subtrair;
//                 Console.WriteLine(op(a, b));
//                 break;
            
//             case 3:
//                 op = Multiplicar;
//                 Console.WriteLine(op(a, b));
//                 break;

//             default:
//                 Console.WriteLine("Opção inválida!");
//                 break;
//         }

//     }
// }

// Exercício 3

