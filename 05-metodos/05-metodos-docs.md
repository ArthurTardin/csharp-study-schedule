# Etapa 5 - Métodos

## Objetivo

Aprender a organizar o código em blocos reutilizáveis: declaração, parâmetros, retorno, sobrecarga, formas de passagem de parâmetros e recursão.

## Declaração e chamada de métodos

Um método é um bloco de código nomeado que executa uma tarefa e pode ser chamado (invocado) quantas vezes for necessária.

```csharp
    static void DizerOla()
    {
        Console.WriteLine("Olá");
    }

    DizerOla(); // chamada
```

- Anatomia:

```csharp
    static int Somar(int a, int b)
    {
        return a + b;
    }
```

- `static`: modificador (por enquantom, todos os métodos serão `static`, sem envolver objetos, isso muda quando chegarmos em POO)
- `int`: Tipo de retorno
- `Somar`: Nome do método
- `(int a, int b)`: parâmetros
- `return a + b;`: valor devolvido a quem chamou

## Parâmetros e retorno

- Se o método devolve um valor, o tipo de retorno é o tipo desse valor, e o método precisa ter um `return`.
- Se não devolve nada, o tipo de retorno é `void`, e o `return` é opcional (pode ser usado sozinho, sem valor, para encerrar o método antes do fim).

```csharp
   static void ImprimirMensagem(string mensagem)
   {
    if (string.IsNullOrEmpty(mensagem))
    {
        return // Encerra aqui, sem imprimir nada
    }

    Console.WriteLine(mensagem);
   } 
```

Por padrão, os parâmetros são passados **por valor**, o método recebe uma cópia. Alterações dentro do método não afetam a variável original (para tipos de valor como `int, double, bool, struct`)

```csharp
   static void Dobrar(int numero)
   {
    numero = numero * 2;
   } 

   int x = 5;
   Dobrar(x);
   Console.WriteLine(x); // Ainda 5
```

## Sobrecarga de método (overload)

Vários métodos podem ter o mesmo nome, desde que a assinatura seja diferente (quantidade, tipo ou ordem dos parâmetros). O compilador decide qual versão chamar com base nos argumentos passados.

```csharp
   static int Somar(int a, int b) => a + b;
   static double Somar (double a, double b) => a + b;
   static int Somar(int a, int b, int c) => a + b + c; 
```

## Parâmetros opcionais e nomeados

- **Opcionais**: Um parâmetro pode ter valor padrão, tornando-o dispensável na chamada. Precisam vir depois do obrigatórios.

```csharp
   static void Saudar(string nome, string saudacao = "Olá")
   {
    Console.WriteLine($"{saudacao}, {nome}!");
   } 

   Saudar("Arthur"); // Olá, Arthur!
   Saudar("Arthur", "Bom dia"); // Bom dia, Arthur!
```

- **Nomeados**: permitem passar argumentos fora da ordem original, referenciando o nome do parâmetro.

```csharp
   Saudar(saudacao: "Boa noite", nome: "Arthur"); 
```

## `ref`, `out` e `in`

- `ref`: Passa a variável **por referência**, o método trabalha com a variável original, e alterações dentro dele refletem fora. A variável precisa estar inicializada antes de ser passada.

```csharp
   static void Dobrar(ref int numero)
   {
    numero = numero * 2;
   } 

   int x = 5;
   Dobrar(x);
   Console.WriteLine(x); // 10
```

`out`: Parecido com `ref`, mas usado quando o método precisa **devolver** um valor através do parâmetro. Não precisa estar inicializada antes de chamar, é o mecanismo usado, por exemplo, no `TryParse`.

```csharp
   static void Dividir(int a, int b, out int resultado, out int resto)
   {
    resultado = a / b;
    resto = a % b;
   } 

   Dividir(10, 3, out int resultado, out int resto);
   Console.WriteLine($"{resultado}, resto {resto}"); // 3, resto 1
```

`in`: passa por referência, mas de forma **somente leitura**, o método recebe acesso direto à variável original (evitando cópia, útil para `structs` grandes), mas não pode modificá-la. Tentar alterar um parâmetro `in` dentro do método gera erro de compilação.

```csharp
   static void MostrarValor(in int numero)
   {
    Console.WriteLine(numero);
    // numero = 10 // ERRO: Não pode alterar um parâmetro 'in'
   } 
```
`params`

Permite que um método receba uma **quantidade variável** de argumentos do mesmo tipo, tratados internamente como um array. Só pode haver um `params` por método, e ele precisa ser o último parâmetro.

```csharp
   static int Somar(params int[] numeros)
   {
    int total = 0;
    foreach(int numero in numeros)
    {
        total += numero;
    }
    return total
   }

   Somar(1, 2); // 3
   Somar(1, 2, 3, 4); // 10
   Somar(); // 0 (nenhum argumento também é válido)  
```

## Recursão

Um método pode chamar a si mesmo. Toda função recursiva precisa de um **caso base** (condição de parada), senão o programa entra em loop infinito e estoura a pilha de execução (`StackOverFlowException`).

```csharp
   static int Fatorial(int n)
   {
    if (n <= 1)
    {
        return 1; //caso base
    }
    return n * Fatorial(n - 1); // Chamada recursiva
   } 
```

Toda recursão pode, em teoria, ser reescrita de forma iterativa (com `for/while`), recursão costuma deixar o código mais legível para problemas naturalmente recursivos (árvores, Fibonacci, fatorial), mas tem um custo extra de memória por causa das chamadas empilhadas na *call Stack*.

## Método de extensão

Permitem "adicionar" um método novo a um tipo já existente (inclusive tipos do próprio .NET, como `string` ou `int`), sem alterar o código-fonte original desse tipo. Precisam ser declarados em uma classe **static**, e o método em si também é **static**, com o primeiro parâmetro usando `this` seguido do tipo que está sendo estendido.

```csharp
   static class StringExtensions
   {
    public static bool IsPalindromo(this string texto)
    {
        string invertido = new string(texto.Reverse().ToArray());
        return texto.Equals(invertido, StringComparison.OrdinalIgnoreCase);
    }
   } 

   //Uso: parece um método nativo da string!
   string palavra = "arara";
   Console.WriteLine(palavra.IsPalindromo()); // true
```

*(Esse tópico será aprofundado mais adiante, por enquanto, o importante é reconhecer o padrão quando aparecer.)*

---

## Exercícios

- [X] Calculadora com método separados (um método por operação: soma,c subtração, multiplicação, divisão)
- [X] Verificador de número primo (versão recursiva e versão iterativa)
- [X] Validador de CPF
- [X] Biblioteca de utilitários matemáticos (MDC, MMC, potência, fatorial, etc, cada um em seu próprio método)