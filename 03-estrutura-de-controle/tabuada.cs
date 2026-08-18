using System;
namespace project03
{
    public class Tabuada
    {
        static void tabuada()
        {
            for (int i = 1; i < 10; i++)
            {
                for (int l = 1; l < 10; l++)
                {
                    Console.WriteLine($"{i} X {l} = {i * l}");
                }
            }
        }
    }
}