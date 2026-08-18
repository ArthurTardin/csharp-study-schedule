using System;

namespace project02
{
    public class Convertor
    {
        public void conversorDeUnidades()
        {
            Console.WriteLine("Conversor de libras para Kilos");
            Console.Write("Digite o peso em libras: ");
            int.TryParse(Console.ReadLine(), out int weight);

            double result = weight / 2.205;

            Console.WriteLine($"Peso em Kilos: {result}");
        }
    }
}