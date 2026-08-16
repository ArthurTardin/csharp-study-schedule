# Etapa 3 - Estruturas de controle

## Objetivo

Aprender a controlar o fluxo de execução do programa através de condicionais e laços de repetição.

## Condicionais

`if`/`else if`/`else`

Estrutura básica para tomar decisões com base em condições booleanas. O bloco correspondente à primeira condição verdadeira é executado; se nenhuma for verdade, o `else` (se existir) é executado.

```csharp
    int idade = 20;

    if (idade < 12)
    {
        Console.WriteLine("Criança");
    }
    else if (idade < 18)
    {
        Console.WriteLine("Adolescente");
    }
    else
    {
        Console.WriteLine("Adulto");
    }
```

`switch`/`switch expression`

O `switch` tradicional compara um valor contra vários casos possíveis. Cada `case` precisa de `break` (ou outro comando de salto) para não "cair" no próximo caso.

```csharp
    switch (diaSemana)
    {
        case 1:
            Console.WriteLine("Domingo");
            break;
        case 2:
            Console.WriteLine("Segunda");
            break;
        default:
            Console.WriteLine("Outro dia");
            break;
    }
```

O `switch expression` (C# 8+) é uma forma mais enxuta, que **retorna um valor diretamente**, sem precisar de `break`:

```csharp
    string nomeDia = diaSemana switch
    {
        1 => "Domingo",
        2 => "Segunda",
        _ => "Outro dia"
    };
```

## Operadores relacionais

Comparam dois valores e retornam um `bool`:

- == -> igual a
- != -> diferente de
- > -> Maior que
- < -> Menor que
- >= -> maior ou igual a
- <= -> Menor ou igual a

## Operadores Lógicos

- && -> E (Ambos lados precisam ser verdadeiros)
- || -> OU (basta um lado ser verdadeiro)
- ! -> NÃO (inverte o valor booleano)

```csharp
    if (idade >= 18 && temCarteira)
    {
        Console.WriteLine("Pode dirigir");
    }
```

- **Um detalhe importante:** `&&` e `||` fazem **short-circuit evaluation** (Avaliação de curto-circuito), se o primeiro operando já define o resultado, o segundo nem é avaliado.

- Em `a && b`: se `a` for `false`, o resultado já é `false` e `b` nunca é checado.
- Em `a || b`: se `a` for `true`, o resultado já é `true` e `b` nunca é checado.

Isso é útil (e às vezes essencial) para evitar erros, como checar se um objeto não é nulo antes de acessar uma propriedade dele:

```csharp
    if (pessoa != null && pessoa.idade >= 18)
    {
        // só acessa pessoa.idade se pessoa não for nulo
    }
```

## Operador ternário
