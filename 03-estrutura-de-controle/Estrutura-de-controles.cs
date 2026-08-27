// Exercício 1

int idade;

Console.Write("Escreva sua idade: ");
int.TryParse(Console.ReadLine(), out idade);

if (idade <= 0)
{
    Console.WriteLine("Digite uma idade válida.");
    return;
}
else if (idade <= 12)
{
    Console.WriteLine("Você é uma criança.");
    return;
}
else if (idade < 18)
{
    Console.WriteLine("Você é um adolescente.");
    return;
}
else if (idade < 65)
{
    Console.WriteLine("Você é um adulto.");
    return;
}
else
{
    Console.WriteLine("Você é um idoso.");
}

// Exercício 2:

for (int i = 1; i < 11; i++)
{
    Console.WriteLine($"Tabuada do {i}:");

    for (int j = 1; j < 11; j++)
    {
        Console.WriteLine($"{i} X {j} = {i * j}");
    }
}

// Exercício 3:

int tentativa;
int numero = 14;

do
{
    Console.Write("Adivinhe o número: ");
    int.TryParse(Console.ReadLine(), out tentativa);

}while(tentativa != numero);

Console.WriteLine("Parabéns, você acertou!");

// Exercício 4:
bool sair = false;
while (sair == false)
{

    Console.WriteLine("1 - Depositar");
    Console.WriteLine("2 - Retirar");
    Console.WriteLine("3 - Ver");
    Console.WriteLine("4 - Sair");
    Console.Write("Escolha uma opção: ");
    int.TryParse(Console.ReadLine(), out int Escolha);

    switch (Escolha)
    {
        case 1:
            Console.WriteLine("Depositado");
            break;
        case 2:
            Console.WriteLine("Retirar");
            break;
        case 3:
            Console.WriteLine("Viu");
            break;
        case 4:
            Console.WriteLine("Saindo...");
            sair = true;
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}