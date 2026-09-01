# Etapa 5 - Métodos

## 1. Por que métodos existem

Métodos é um bloco de código nomeado e reutilizável. Sem métodos, todo código vira uma sequência linear gigante, repetida toda vez que a mesma lógica é necessária. Você já usou métodos o tempo todo sem perceber, `Console.WriteLine`, `int.TryParse`, `List.Add` são todos métodos, só que definidos pela biblioteca do .NET, não por você.

```Csharp
   static int Somar(int a, int b)
   {
      return a + b;
   }

   int resultado = Somar(5, 3); // 8
```

Partes de um método:

- **Modificador**: `static`, por enquanto sempre vai ser assim até a Etapa 6.
- **Tipo de retorno**: `int`, o que o método devolve, `void` se não devolve nada.
- **Nome**: `Somar`, convenção em C# é PascalCase para métodos.
- **Parâmetros**: `int a, int b`, os dados que o método recebe.
- **Corpo**: O que ele faz.
- **`return`**: devolve o valor, obrigatório se o tipo de retorno não é `void`, proibido ter valor se for `void`.

---

## 1. Parâmetros

### Parâmetros posicionais (padrão)

```Csharp
   static void Apresentar (string nome, int idade)
   {
      Console.WriteLine($"{nome} tem {idade} anos.");
      Apresentar("Arthur", 17); // ordem importa
   }
```

Se você inverter a ordem do argumento na chamada (`Apresentar(17, "Arthur")`), isso é erro de compilação (tipos não batem), nesse caso específico o compilador te protege. Mas se os dois parâmetros foram do mesmo tipo (`Apresentar(string nome, string sobrenome)`), inverter a ordem compila normalmente e dá resultado errado silenciosamente. Isso é o tipo de bug que só aparece rodando, não compilando, fica atento.

### Parâmetros nomeados

```Csharp
   Apresentar(idade: 17, nome: "Arthur"); //Ordem não importa mais, porque está nomeado
```

Útil quando o método tem muitos parâmetros e você quer deixar claro na chamada o que é o quê, sem depender de memorizar a ordem.

### Parâmetros opcionais (com valor default)

```Csharp
   static void Saudacao(string nome, string saudacao = "Olá")
   {
      Console.WriteLine($"{saudacao}, {nome}!");
   }

   Saudacao("Arthur"); // Olá, Arthur!
   Saudacao("Arthur", "Bom dia") // Bom dia, Arthur!
```

Regra: Parâmetro opcional, sempre vem depois dos obrigatórios na assinatura do método, não dá para ter opcional antes de obrigatório.

## 3. Sobrecarga de método (overload)

Mesmo nome de método, assinaturas diferentes (quantidade ou tipo de parâmetros diferente):

```Csharp
   static int Somar(int a, int b) => a + b;
   static double Somar(double a, double b) => a + b;
   static int Somar(int a, int b, int c) => a + b + c;
```

O compilador decide qual versão chamar beseado nos tipo/quantidade de argumentos que você passa. Isso é diferente de `default` de parâmetro, overload são métodos genuinamente distintos, só que compartilhando nome.
(A sintaxe `=> a + b` acima é expression body, forma compacta de método de uma linha só, equivalente a `{return a + b; }`)

---

## 4. ref, out, int: Modificadores de parâmetro

Por padrão, tipos primitivos (`int`, `double`, `bool` e etc) são passados por valor, o método recebe uma cópia, alterações dentro dele não afetam a variável original:

```Csharp
   static void Dobrar(int numero)
   {
      numero = numero * 2; // só altera a cópia local
   }

   int x = 5;
   Dobrar(x);
   Console.WriteLine(x); // ainda imprime 5 -- não mudou
```

### ref: passagem por referência

```Csharp
   static void Dobrar(ref int numero)
   {
      numero = numero * 2;
   }

   int x = 5;
   Dobrar (ref x);
   Console.WriteLine(x); // Agora imprime 10
```

`ref` faz o método operar na variável original, não numa cópia. Precisa do `ref` tanto na assinatura do método quando na chamada, não é opcional em nenhum dos dois lados. Exige que a variável já tenha valor antes de passar.

### out: você já usou isso

```Csharp
   bool sucesso = int.TryParse("42", out int numero);
```

`out` é parecido com `ref`, mas com uma diferença chave: a variável não precisa ter valor antes, o método é obrigado a atribuir um valor a ela antes de terminal. É o padrão que `TryParse` e `TryGetValue` usam para devolver um valor extra, além do retorno principal (o bool de sucesso).

### in: menos comum, mas vale saber que existe

Passa por refêrencia, mas read-only dentro do método, usado principalmente por performance em `struct` grandes (assunto futuro), pra evitar cópia sem permitir alteração acidental. Não é algo que você vai usar ativamente agora, só reconhecer se ver em código de terceiro.

Regra prática: Não use `ref/out` só porque parece "avançado". Uso apenas quando o método genuinamente precisa devolver mais de um valor, ou modifica a variável orginal do chamador, Na dúvida, prefira retornar um valor normal (`return`), é mais simples de ler.

---

## 5. Recursão

Um método que chama a si mesmo. Toda função recursiva precisa de duas partes:

1. **Caso-base**: condição que para a recursão. Sem isso, o método chama a si mesmo infinitamente.
2. **Caso recursvo**: A chamada do método a si mesmo, progredindo em direção a caso-base.

```Csharp
   static int Fatorial(int n)
   {
      if (n <= 1) return 1; // caso-base
      return n * Fatorial(n - 1); //caso recursivo -- progride em direção ao caso-base
   }
```

O que acontece sem caso-base (ou com caso-base que nunca é alcançado):

```Csharp
   static int FatorialQuebrado(int n)
   {
      return n * FatorialQuebrado(n - 1); //nunca para
   }
```

Isso não trava silenciosamente igual loop infinito, cada chamada recursiva empilha um novo "quadro" na pilha de execução (call stack), que tem tamanho limitado. Isso estoura em `StackOverFlowException`, geralmente derrubando o programa inteiro de forma abrupta (esse tipo de exceção nem sempre é capturável com try/catch normal, depende do ambiente).

Por que recursão existe se dá para fazer com loop: alguns problemas são naturalmente recursivos em estruturas (percorrer uma árvore de pastas, uma estrutura hierárquica), a versão recursiva fica mais legível que a versão com loop equivalente. Mas recursão tem custo de memória (pilha de chamadas) que loop não tem, não é "melhor" por padrão, é uma ferramenta com trade-off

---

## Checklist antes de ir pros exercícios

- [x] Eu sei por que inverter a ordem de dois parâmetros do mesmo tipo pode compilar normalmente e ainda assim estar errado?
- [x] Eu sei a diferença prática entre `ref` e `out`, quando a variável precisa ter valor antes de passar, e quando não precisa?
- [x] Eu sei nomear as duas partes obrigatórias de toda função recursiva, e o que acontece se uma delas faltar?
- [x] Eu sei por que `StackOverFlowException` de recursão sem caso-base é diferente de um loop infinito comum?

---

## Exercícios

1. **Validador de CPF**: método que recebe uma string de CPF e retorna `bool` indicando se tem o formato válido (11 dígitos numéricos, não precisa validar dígito verificador ainda, só formato)
2. **Biblioteca de utilitários matemáticos**: Crie métodos separados: `EhPar(int n)`, `EhPrimo(int n)`, `MDC(int n, int b)`
3. **Fatorial recursivo**: implemente `Fatorial(int n)` com recursão, incluindo caso-base correto;
4. **Fibonacci recursivo**: Implemente `Fibonacci(int posicao) que retorna o n-ésino número da sequência de Fibonacci, usando recursão.
5. **Sobrecarga prática**: crie 3 versões sobrecarregadas de um método `Maior`: Uma para 2 `int` para 2 `double`, uma para 3 `int`

- **[DEBUG]**

Dois métodos quebrados abaixo, um trava por recursão sem caso-base correto, outro usa `ref`/`out` de forma incorreta e não compila ou dá resultado errado. Ache e corrija ambos.

```Csharp
   static int Somatorio(int n)
{
    return n + Somatorio(n - 1);
}

static void Trocar(int a, int b)
{
    int temp = a;
    a = b;
    b = temp;
}

int x = 5;
int y = 10;
Trocar(x, y);
Console.WriteLine($"x = {x}, y = {y}"); // deveria imprimir x = 10, y = 5
```