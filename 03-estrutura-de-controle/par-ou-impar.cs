using System;
namespace project03;
public class ParouImpar
{
    public static void ParouImpa(string[] args)
    {
        Console.Write("Escreva um número: ");

        if (int.TryParse(Console.ReadLine(), out int result))
        {
            if (result % 2 == 0)
            {
                Console.WriteLine("O número é par");
            }
            else 
            {
                Console.WriteLine("O número é ímpar");
            } 
        }
        else
        {
            Console.WriteLine("Entrada inválida! Digite apenas números inteiros.");
        }
    }
}