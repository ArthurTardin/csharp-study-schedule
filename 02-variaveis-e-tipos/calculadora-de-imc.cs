using System;

namespace Exercicios
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculadora de IMC:");

            Console.Write("Digite seu peso: ");
            double.TryParse(Console.ReadLine(), out double weight);

            Console.Write("Digite sua altura: ");
            double.TryParse(Console.ReadLine(), out double height);

            int resultadoImc = (int)weight / ((int)height * (int)height);

            if (resultadoImc < 18)
            {
                Console.WriteLine("Magreza.");
            }
            else if (resultadoImc <= 24)
            {
                Console.WriteLine("Peso normal.");
            }
            else if (resultadoImc <= 29)
            {
                Console.WriteLine("Sobrepeso");
            }
            else if (resultadoImc <= 34)
            {
                Console.WriteLine("Obesidade grau I");
            }
            else
            {
                Console.WriteLine("Obesidade grau II");
            }
        }
    }
}