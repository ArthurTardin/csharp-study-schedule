# Etapa 2 - Variáveis e Tipos

Parte da minha jornada de aprendizado em C#. Aqui documento os conceitos estudados, exemplos práticos e exercícios feitos nesta etapa.

## Objetivo da Etapa

Entender como C# lida com dados: Os tipos primitivos, como declarar e usar variáveis, e como converter valores entre tipos diferentes.

## Tipos de dados

C# é uma linguagem **fortemente tipada e estaticamente tipada**, toda variável precisa ter um tipo definido em tempo de compilação.

### Tipos numéricos inteiros

1. Byte - 8 bits - 0 a 255
2. short - 16 bytes - 32.768 a 32.767
3. int - 32 bits ~-2,1bi a 2,1bi
4. long - 64 bits número bem maior, usado para valores grandes

```csharp
    byte idade = 25;
    short anoNascimento = 1999;
    int populacao = 2000000;
    long distanciaEstrelas = 9460730472580800;
```

### Tipos númericos com casas decimais

1. float - ~7 dígitos - pouca precisão, usa sufixo f
2. double - ~15-16 dígitos - padrão para cálculos com decimais
3. decimal - ~28-29 dígitos - valores monetários (mais preciso)

```csharp
    float altura = 1.75f;
    double pi = 3.14159265359;
    decimal preco = 19.90m // "m" indica decimal
```

- **Dica prática**: para dinheiro, sempre usar `decimal`. Float e double têm erros de arredondamento por causa de como representam números binários.

### Outros tipos básicos

```csharp
    bool ativo = true;
    char letra = 'a'; //aspas simples, um único caractere
    string nome = "Arthur"; //aspas duplas, texto
```

### Conceito principal

#### Declaração, inicialização e atribuição

```csharp
    int idade; // declaração (ainda sem valor)
    idade = 20; // Atribuição

    int altura = 180; // declaração + inicialização na mesma linha
```

#### Constantes

Valores que não podem ser alterados depois de definidos:]

```csharp
    const double PI = 3.14159;
```

#### `var` - inferência de tipo

O compilador descobre o tipo sozinho com base no valor atribuído. O tipo continua fixo, só que quem escreveu não precisou declará-lo explicitamente:

```csharp
    var nome = "Arthur"; // vira string
    var idade = 22; // vira int
    var altura = 1.75; // vira double
```

`var` precisa de um valor inicial na mesma linha, senão o compilador não tem como inferir o tipo.

#### Escopo

Onde a variável "existe" e pode ser usada. Uma variável declarada dentro de um método ou bloco { } só existe ali dentro:

```csharp
    void Metodo()
    {
        int x = 10; // x só existe dentro deste método
    }
    // aqui fora, x não existe mais
```

#### Conversão de tipos

##### Casting implícito (automático)

Quando não há risco de perda de dados, o C# converte sozinho:

```csharp
    int numero = 10;
    double numeroDouble = numero; // int para double, sem problema
```

##### Casting explícito

Quando pode haver perda de dados, é preciso "forçar" a conversão:

```csharp
    double valor = 9.8;
    int valorInt = (int)valor; // valorInt vira 9 (perde a parte decimal)
```

##### `Parse` e `TryParse`

Usados para converter `string` em número, muito comum ao ler dados do `Console.ReadLine()`, que sempre retorna `string`.

```csharp
    string text = "25";

    //Parse: quebra o programa (exceção) se o texto não for um número válido
    int numero = int.Parse(text);

    //TryParse: Mais seguro, retorna true/false e não quebra o programa
    bool sucesso = int.TryParse(text, out int resultado);

    if (sucesso)
    {
        Console.WriteLine($"Convertido: {resultado}");
    }
    else
    {
        Console.WriteLine("Não foi possível converter.");
    }
```
- **Boa prática**: Sempre preferir `TryParse` quando o valor vem de uma entrada do usuário, porque `Parse` pode derrubar o programa se o texto não for válido.

---

## Exercícios

- [x] IMC
- [x] Média escolar
- [x] Conversor de moedas
- [X] Conversor de unidade
- [X] Cálculo de salário
