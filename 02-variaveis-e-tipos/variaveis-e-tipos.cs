// Exercício 1:

Console.Write("Digite o seu peso (kg): ");
double kg;
if (!double.TryParse(Console.ReadLine(), out kg))
{
    Console.WriteLine("Valor inválido.");
    return;
}

Console.Write("Digite sua altura: ");
double.TryParse(Console.ReadLine(), out double altura);

double IMC = kg / (altura * altura);

Console.WriteLine($"Seu IMC é: {IMC}");

// Exercício 2:

Console.Write("Digite a nota 1: ");
double.TryParse(Console.ReadLine(), out double nota1);

Console.Write("Digite a nota 2: ");
double.TryParse(Console.ReadLine(), out double nota2);

Console.Write("Digite a nota 3: ");
double.TryParse(Console.ReadLine(), out double nota3);

double media = (nota1 + nota2 + nota3) / 3;

Console.WriteLine($"Sua média é: {media}");

// Exercício 3:

Console.Write("Digita o valor em Reais: ");
double reais;
if (!double.TryParse(Console.ReadLine(), out reais))
{
    Console.WriteLine("Número inválido");
    return;
}

Console.WriteLine($"Seu valor convertido para dolar é: {reais * 5.16}");

// Exercício 4:

Console.Write("Digite o valor(M): ");
double.TryParse(Console.ReadLine(), out double metros);

Console.WriteLine($"Seu valor em CM é: {metros * 100} e em KM: {metros / 1000}");

// Exercício 5:

const int mesBase = 220;
Console.Write("Digite o seu salário base: ");
double.TryParse(Console.ReadLine(), out double salarioBase);

Console.Write("Digite as horas extras trabalhadas: ");
int.TryParse(Console.ReadLine(), out int horasExtras);

double ganhoHora = salarioBase / mesBase;
Console.WriteLine($"Seu salário líquido é: {salarioBase + (horasExtras * ganhoHora * 1.5)}");




