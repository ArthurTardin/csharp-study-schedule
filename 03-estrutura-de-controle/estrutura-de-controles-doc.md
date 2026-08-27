# Etapa 3 - Estrutura de Controle

## 1. Estruturas condicionais

if/else if/else

```Csharp
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

Ponto que costuma gerar bug silencioso: a ordem das condições importa quando elas se sobrepõem. `else if (idade < 18)` só é avaliado se a primeira condição (`idade < 12`) for falsa, então dentro daquele bloco, você já sabe implicitamente que `idade >= 12`. Gente iniciante às vezes escreve `else if (idade >= 12 && idade < 18)` redundantemente, o que não é erro, mas mostra que não confiou na estrutura em cascata.

### Operadores ternários

```Csharp
   string resultado = idade >= 18 ? "adulto" : "menor de idade"; 
```

Forma compacta de if/else quando o resultado é só atribuir um valor. Legível até certo ponto, ternário aninhado (`condição ? a : condição2 ? b : c`) vira ilegível rápido. Evite aninhar.

### Switch

```Csharp
   int diaSemana = 3;
   string name;

   switch (diaSemana)
   {
    case 1:
        nome = "segunda";
        break;
    case 2:
        nome = "Terça";
        break;
    default:
        nome = "dia inválido";
        break;
   } 
```

`break` é obrigatório em C# ao final de cada `case` com código (diferente de linguagens que fazem fallthrough automático), esquecer o `break` gera **erro de compilação** em C# (não bug silencioso como em C/JavaScript), então isso é uma rede de segurança do próprio compilador.

### switch expression (moderno, C#8+)

```Csharp
   string nome = diaSemana switch
   {
    1 => "segunda",
    2 => "Terça",
    _ => "Dia inválido"
   };
```

Mais compacto, sem `break`, o `_` é o "default". Você vai ver as duas formas em código real, a moderna é preferida em projetos novos.

### Operadores lógicos e de comparação

- `=` - igual a
- `!=` - diferente de
- `&&` - E lógico (ambos precisam ser verdadeiros)
- `||` - OU lógico (pelo menos um verdadeiro)
- `!` - Negação

**Short-circuit evaluation**, importante e frequentemente ignorado por iniciantes: em `a && b` se `a` já for falso, C# nem avalia `b`. Em `a || b`, se `a` já for verdadeiro, `b` ne é avaliado. Isso não é só otimização, é usado propositalmente pra evitar erro, por exemplo:

```Csharp
   if (!= null && lista.Count > 0) 
```

Se `lista` for `null`, a primeira condição já é false e `lista.count` **nunca é avaliado**, evitando um erro de tentar acessar propriedade de algo nulo. Se você inverter a ordem (`lista.Count > && lista != null`), o programa quebra quando `lista` for `null`, porque `lista.Count` seria avaliado primeiro. Ordem importa.

## 2. Estruturas de repetição

```Csharp
   for (int i = 0; i < 5; i++)
   {
        Console.WriteLine(i);
   } 
```

Três partes: inicialização (`int i = 0`), condição de continuação (`i < 5`), incremento (`i++`). Usado você **você sabe quantas vezes** quer repetir (ou tem um índice claro para controlar).

Erro clássico de iniciante: condição `1 <= 5` imprime 6 valores (0 a 5), não 5. Sempre confira se a condição de parada bate com a quantidade real que você quer.

### while

```Csharp
   int contador = 0;
   while (contador < 5)
   {
        Console.WriteLine(contador);
        contador++;
   } 
```

Usado quando você **você não sabe** quantas iterações vai precisar de antemão, a repetição depende de uma condição que muda por lógica externa (ex: usuário digitando "sair").

**Risco real:** Esquecer de atualizar a variável de controle dentro do loop gera **loop infinito**. Isso não dá erro de compilação, o programa simplesmente trava rodando pra sempre. É o tipo de bug que você vai caçar no exercício de debug desta etapa.

### do-while

```Csharp
   int opcao;
   do
   {
    Console.WriteLine("Digite uma opção: ");
    opcao = int.Parse(Console.ReadLine());
   } while (opcao != 0);
```

Diferença chave pro `while`: o bloco executa **Pelo menos uma vez**, mesmo que a condição já comece falsa, porque a condição só é checada **depois** do bloco rodar. Útil pra menus: você sempre que mostrar o menu ao menos uma vez antes de checar se o usuário quer sair.

### Break e continue

- `break` - sai do loop imediatamente, ignorando o resto das iterações.
- `continue` - Pula pro próximo ciclo do loop, ignorando o resto do código daquela iteração específica, mas sem sair do loop inteiro.

```Csharp
   for (int i = 0; 1 < 10; i++)
   {
    if (i == 5) break; // Para no 5
    if (i % 2 == 0) continue; // Pula os pares, mas continua o loop
    Console.WriteLine(i);
   } 
```

### loop aninhado

```Csharp
   for (int i = 1. i <= 3; i++)
   {
    for (int j = 1; j <= 3; j++)
    {
        Console.WriteLine($"{i}, {j}")
    }
   } 
```

Cuidado com a complexidade: Dois loops aninhado de tamanho N roda NxN vezes. Isso ainda não importa em exercício pequeno, masé o ínicio de entender por que performance de algoritmo import , assunto que você volta com mais peso mais pra frente.

---

## Checklist antes de ir pros exercícios

- [X] Eu sei explicar por que `lista != null && lista.count? > 0` funciona, mas a ordem invertido quedra?
- [x] Eu sei por que `do-while` roda pelo menos uma vez mesmo com a condição falsa de cara?
- [X] Eu sei diferenciare quando usar `for`(sei quantas vezes) vs `while` (não sei quantas vezes);
- [X] Eu sei identificar, olhando um while, se existe um risco de loop infinito (variável de controle não está sendo atualizada dentro do bloco)?

---

## Exercícios

1. **classificador de idade**: leia idade, classifique em criança/adolescente/adulto/idoso usando if/else if.
2. **Tabuada**: leia um número, imprima a tabuada de 1 a 10 usando `for`
3. **Jogo de adivinhação de número**: O programa "pensa" em um número fixo (ex: 7), o usuário tenta adivinhar em loop até acertar, usando `while` ou `do-while`.
4. **Menu de opções no console**: Menu com pelo menos 3 opções + "sair", rodando em loop até o usuário escolher sair, usando `switch`

### [DEBUG] - primeiro exercício de debug real

Abaixo está um código de menu de console com **bugs propositais**.

```Csharp
   int opcao = -1;

   while (opcao = 0)
   {
        Console.WriteLine("1 - Ver saldo");
        Console.WriteLine("2 - Depositar");
        Console.WriteLine("3 - Sair");
        Console.Write("Escolha: ");
        opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                Console.WriteLine("Seu saldo é: 1000");
            case 2:
             A   Console.WriteLine("Depósito realizado.");
                break;
            case 3:
                Console.WriteLine("Saindo...");
                break;
            default:
                Console.WriteLine("Opção inválida.")
                break;
        }
   } 
```

Dica única: Compile antes de sair caçando bug de lógico.