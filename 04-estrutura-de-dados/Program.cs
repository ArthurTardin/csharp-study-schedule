using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace Project04
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListaDeCompras();
        }

        public static void ListaDeCompras()
        {
            List<string> compras = new List<string>{"Arroz", "Feijão", "Salada", "Carne"};
            Console.WriteLine("Lista de compras: ");

            Console.WriteLine("1 - Adicionar produto");
            Console.WriteLine("2 - Remover produto");
            Console.WriteLine("3 - Ver lista");

            Console.Write("Escolha sua opção: ");
            int.TryParse(Console.ReadLine(), out int choice);

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Digite o nome do produto:");
                    string add = Console.ReadLine()!;

                    compras.Add(add);
                    break;
                case 2:
                    Console.WriteLine("Digite o nome do produto");
                    string rm = Console.ReadLine()!;

                    compras.Remove(rm);
                    break;
                case 3:
                    foreach(string item in compras)
                    {
                        Console.WriteLine(item);
                    }
                    break;
                default:
                    Console.WriteLine("Escolha uma opção válida.");
                    break;
            }
        }

        public static void ListaDeContatos()
        {
            Dictionary<string, long> contatos = new Dictionary<string, long>();
            Console.WriteLine("1 - Adicionar contato");
            Console.WriteLine("2 - remover contato");

            int.TryParse(Console.ReadLine(), out int choice);

            switch (choice)
            {
                case 1:
                    contatos.Add("Arthur", 2312332423432423);
                break;
    
                case 2:
                    contatos.Remove("Arthur");
                break;


                default:
                    Console.WriteLine("Inválido");
                break;
            }
        }
    }
}