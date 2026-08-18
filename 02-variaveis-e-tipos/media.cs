using System.Numerics;
using System;

public class Media
{
    static void media()
    {
    Double[] valoreHabilidades = new double[] { 25.34, 35.76, 34.12, 10, 40 };

    double soma = 0;
    int TotaldeIndice = 0;

    for (int i = 0; i < valoreHabilidades.Length; i++)
    {
        TotaldeIndice = i + 1;

        soma = soma + valoreHabilidades[i];
    }
    Console.WriteLine("Os valores das habilidades do arqueiro são:");
    Console.WriteLine($"O total de índice é: {TotaldeIndice}");
    Console.WriteLine($"A soma dos índice é: {soma}");
    double result = soma / TotaldeIndice;
    Console.WriteLine($"A média dos danos das habildiadees é: {result}");
    }   

}