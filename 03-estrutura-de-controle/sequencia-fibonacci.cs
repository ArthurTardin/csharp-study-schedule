//Sintex Padrão
using System.Threading;
namespace project03
{
    public class Sequencia
    {
        public static void Fibonacci()
        {
            //Utilizar emojis
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Repetição
            bool validInput = true;
            while (validInput)
            {
                //Variáveis para armazenar os dois Primeiros termos da sequência
                long firstTerm = 0, secondTerm = 1;
                //Tela Inicial
                Console.WriteLine("======Sequência de Fibonacci======");

                //Solicitar ao usuário o número de termos
                Console.Write("Digite o número de termos: ");
                int.TryParse(Console.ReadLine()!, out int terms);

                //gerar a sequência de Fibonacci
                for (int i = 0; i < terms; i++)
                {
                    Console.Write(firstTerm + "➡️ ");
                    long  nextTerm = firstTerm + secondTerm;
                    firstTerm = secondTerm;
                    secondTerm = nextTerm;
                }
                //Pausa para o usuário ver a sequência gerada
                Thread.Sleep(5000);
                Console.WriteLine("\n Deseja escolher novamente?[S/N]");
                string userChoice = Console.ReadLine()!.ToUpper();

                //Verificar a escolha do usuário para continuar ou encerrar o programa
                if (userChoice != "S")
                {
                    validInput = false;
                    Console.WriteLine("\n Programa Encerrado.");
                }
                else
                {
                    validInput = true;
                    Console.Clear();
                }
            }
        }   
    }
}