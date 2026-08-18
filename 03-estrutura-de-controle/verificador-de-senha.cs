using System;
namespace project03
{
    public class passWord
    {
        public static void Verifica(string[] args)
        {
            int passWord = 1234567;

            Console.WriteLine("Digite a senha:");
            int.TryParse(Console.ReadLine(), out int result);

            if (result != passWord)
            {
                Console.WriteLine("Senha incorreta.");
            }
            else
            {
                Console.WriteLine("Senha correta.");
            }
        }
    }
}
