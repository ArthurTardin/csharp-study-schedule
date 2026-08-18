using System;
namespace project02
{
    public class Salario
    {
        static void calculo()
        {
            Console.Write("Digite o seu salário bruto: ");
            decimal.TryParse(Console.ReadLine(), out decimal result);

            decimal final = result - 1500;
            Console.WriteLine(final);
        }
    }
}