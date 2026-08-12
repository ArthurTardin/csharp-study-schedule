/*
    Exercício 1 - Crie um programa que imprima na tela a seguinte mensagem: "Hello, World!".
*/
Console.WriteLine("Hello, World!");
//Imprime Hello world na tela

/*
    Exercício 2 - Crie um programa que declare uma variável do tipo string chamada "name" e atribua a ela o valor "Arthur". Em seguida, imprima na tela a mensagem: "Olá, Arthur!".
*/
string name = "Arthur";
Console.WriteLine("Olá, " + name + "!"); //Imprime Olá, Arthur! na tela

/*
    Exercício 3 - Crie um programa que declare uma variável do tipo int chamada "age" e atribua a ela o valor 17. Em seguida, imprima na tela a mensagem: "Você tem 17 anos.".
*/

int age = 17;
Console.WriteLine("Você tem " + age + " anos."); //Imprime Você tem 17 anos. na tela

/*
    Operações matemática
    - + soma
    - - subtração
    - * multiplicação
    - / divisão
    - % resto da divisão
*/

// 1 + 1; //2
// 1 - 1; //0
// 2 * 2; //4
// 2 / 2; //1
// 10 % 3; //1


/*
    Exercício 4 - Crie um coversor de temperatura que converta graus Celsius para Fahrenheit. A fórmula para conversão é: F = C * 9/5 + 32.
*/
double celsius = 25;
double fahrenheit = celsius * 9/5 + 32;
Console.WriteLine(fahrenheit); //Imprime 77 na tela
