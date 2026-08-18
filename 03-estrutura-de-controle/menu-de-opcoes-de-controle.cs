using System;
namespace project03
{
    public class Menu
    {
        static void controle()
        {
            Console.WriteLine("Escolha entre 1 a 10");
            int.TryParse(Console.ReadLine(), out int result);

            switch (result)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
}