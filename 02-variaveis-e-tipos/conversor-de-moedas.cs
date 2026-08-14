//Sintex Padrão
using System;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace programaIMC
{
    public class Program
    {
        public static void Main()
        {
            bool isRunning = true;
            while (isRunning)
            {
                // Declaração de um array de números decimais (double) para armazenar taxas de juros
                double[] currencyRate =
                {
                5.18, 5.97, 0.032, 6.84, 6.48, 0.75, 3.56, 4.73
                };

                //Declaração de um array de strings para armazenar os nomes das moedas
                string[] currencyName =
                {
                "Dólar Americano",
                    "Euro",
                    "Iene Japonês",
                    "Libra Esterlina",
                    "Franco Suíço",
                    "Renminbi/Yuan",
                    "Dólar Australiano",
                    "Dólar Canadense"
                };

                //Tela Inicial
                Console.WriteLine("======Convertor de Moedas======");

                // Exibe as taxas de juros e os nomes das moedas
                for (int i = 0; i < currencyName.Length; i++)
                {
                    Console.WriteLine($"\n{i + 1}: {currencyName[i]}");
                }

                // Solicita ao usuário que escolha uma moeda
                Console.WriteLine("\nSelecione a moeda para a qual deseja converter:");

                //Armazenar a escolha do usuário
                int.TryParse(Console.ReadLine(), out int choice);

                int index = choice - 1; // Ajusta o índice para acessar os arrays

                //Validando e Calculando a conversão
                if (index >= 0 && index < currencyName.Length)
                {
                    // amarzenar o valor em reais para conversão
                    Console.WriteLine("Digite um valor em reais para converter:");
                    double.TryParse(Console.ReadLine(), out double reais);

                    // Realiza a conversão usando a taxa de juros selecionada
                    Double result = reais / currencyRate[index];

                    // Exibe o resultado da conversão
                    Console.WriteLine($"O valor em {currencyName[index]} é: {result:F2}");

                    // Pergunta ao usuário se deseja realizar outra conversão
                    Console.WriteLine("Deseja realizar outra conversão? (s/n)");
                    string continueChoice = Console.ReadLine()!.ToLower();

                    // Verifica a resposta do usuário para continuar ou encerrar o programa
                    if (continueChoice != "s")
                    {
                        isRunning = false; // Encerra o loop para finalizar o programa
                    }
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Opção inválida. Por favor, selecione uma moeda válida.");
                    Thread.Sleep(2000); // Pausa por 2 segundos para permitir que o usuário leia a mensagem
                    Console.Clear(); // Limpa a tela para uma nova tentativa
                    continue; // Volta para o início do loop para permitir uma nova tentativa
                }
            }
        }
    }
}