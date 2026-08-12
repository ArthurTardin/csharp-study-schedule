# 1 `Main`

Históricamente, o ponto de entrada de um programa C# era o método:
```csharp
    static void Main()
    {
        Console.WriteLine("Hello World!");
    }
```
O `Main` significa, essencialmente: 
**A execução do programa começa aqui**

Vamos quebrar:
```csharp
    static void Main()
```
## `static`

Significa que o método pertence à classe, e não a uma instância específica dela.

## `void`

Indica que o método não retorna valor.

## `Main`

É o nome do método que funciona como ponto de entrada tradional da aplicação

## Ponto importante!

Nas versões modernas do C#, você pode escrever simplesmente:
```csharp
    Console.WriteLine("Hello World!");
```
e o programa funciona. Isso acontece por causa dos **top-level statements**. O compilador trata esse código como uma aplicação cujo ponto de entrada é gerado automaticamente, portanto:

### Forma tradicional:

```csharp
    class Program{
        static void Main(){
            Console.WriteLine("Hello World!");
        }
    }
```

### Forma moderna:

```csharp
    Console.WriteLine("Hello World!");
```

---

# 2 `Console.WriteLine()`

É uma das primeiras coisas que se utiliza em C#.
```csharp
    Console.WriteLine("Olá, mundo!");
```

Ele escreve algo no terminal e **Pula para a próxima linha**
Por exemplo:
```csharp
    Console.WriteLine("olá!");
    Console.WriteLine("Meu nome é Arthur");
    Console.WriteLine("Estou aprendendo C#.");
```

resultado:
```text
    Olá!
    Meu nome é Arthur
    Estou aprendendo C#.
```

`Console`: Representa o console

`WriteLine()`: Manda uma mensagem para o console e adiciona uma quebra de linha no final.

---

# 3 `Console.Write()`

É parecido com `WriteLine()`, mas **não pulo para a próxima linha**.

---

# 4 Comentários

Comentários são textos dentro do código que não são executados pelo programa. Servem para explicar o código, registrar observações ou desativar temporariamente algum trecho.

## Comentário de uma linha
```csharp
    //
```

Exemplo:
```csharp
    //Este programa mostra uma mensagem na tela
    Console.WriteLine("Olá!");
```

## Comentário de várias linhas
```csharp
    /*
        comentário
        de várias
        linhas
    */
```

Exemplo:
```csharp
    /*
        Este programa
        foi criado para
        estudar os primeiros
        conceitos de C#
    */
```

**NÃO COMENTE ABSOLUTAMENTE TUDO, DEVEM EXPLICAR O QUE NÃO É ÓBVIO, E NÃO NARRAR CADA LINHA.**

---

# 5 Estrutura de um programa

**Versão moderna**:
```csharp
    Console.WriteLine("olá!");
    Console.WriteLine("Meu nome é Arthur");
    Console.WriteLine("Estou aprendendo C#.");
```

Cada instrução normalmente termina com:
```csharp
    ;
```
Isso indica final da instrução.

`{}`Servem para delimitar blocos:
Exemplo:
```csharp
    if (idade >= 18){
        Console.WriteLine("maior de idade.");
    }
```