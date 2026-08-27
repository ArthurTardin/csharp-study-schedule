# Etapa 2 - Variáveis e Tipos

## 1. Tipos primitivos

C# é **fortemente tipado**, toda variável tem um tipo fixo, definido em tempo de compilação, e isso não muda depois (tirando conversões explícitas).

- `byte` - 8 bits - 0 a 255 - Valores pequenos e sempre positivos (ex: idade, nivel 0 - 100)
- `short` - 16 bits - -32.768 a 32.767 - Raramente usado, economia de memória em cenários específicos
- `int` - 32 bits - ~-2,1 bi a 2,1 bi - **padrão** para números inteiros no dia a dia
- `long` 64 bits - Muito maior - quando `int` pode estourar (ex: contagem de milissegundos desde 1970)

Regra prática: use `int` por padrão. Só desde pra `byte`/`short` ou sobe `long` quando tiver razão específica (memória crítica ou risco real de overflow)

### Tipos numéricos com casas decimais

- `float` - ~7 dígitos, 32 bits - Raro no dia a dia, precisão baixa
- `double` ~15-16 dígitos, 64 bits - **padrão** para decimais em geral
- `decimal` - 28-29 dígitos, alta precisão - **Dinheiro. Sempre**

Isso não é regra arbitrária: `float` e `double` são baseados em representação binária de ponto flutuante, que **não representa certos decimais com exatidão** (o clássico `0.1 + 0.2 =/ 0.3` existe em quase toda linguagem por esse motivo). `decimal` usa uma representação diferente, exata para base 10, por isso é o tipo certo para valores monetários. Usar `double` para dinheiro é erro comum que gera diferença de centavos acumulado em sistema reais.

### bool

Só `true` ou `false`. Sem "verdadeiro implícito" tipo 0/1 como em C, em C# você não pode atribuir `int` a `bool` nem usar `int` como condição.

### char

Um único caractere Unicode, entre aspas simples: `char letra = 'A';`. Não confundir com `string` (aspas duplas), que é uma sequência de caracteres.

### string

Sequência de caracteres, imutável, toda operação que "modifica" uma string na verdade cria uma nova string na memória. Isso importa quando você começar a concatenar strings em loop grande (assunto para mais pra frente, mas guarde a palavra: `StringBuilder`).

---

## 2. Declaração, inicialização e atribuição

```Csharp
    int idade; // declaração (existe, mas sem valor definido ainda)
    idade = 17; // atribuição (dá um valor a uma variável já declarada)
    int altura = 180; // declaração + inicialização na mesma linha
```

Diferença que trava iniciante: uma variável **declarada, mas não inicializada** não pode ser lida, o compilador barra isso (ao contrário de outras linguagens que dão valor padrão silencioso tipo `0` ou `null`). Isso é proposital: evita bug de "esqueci de dar valor".

### Constantes

```Csharp
    const double PI = 3.14159; 
```

Valor fixo em tempo de **compilação**, não pode mudar nunca, nem depois. Diferente de uma variável comum que só "não muda porque você decidiu não mudar".

### var

```Csharp
   var idade - 17; // O compilador infere que é int, baseado no valor  
```

`var` não é tipagem dinâmica, o tipo ainda é fixado em tempo de compilação, só que **inferido** pelo valor à direita, em vez de você escrever explicitamente. Depois de declarado com `var`, o tipo real por trás não muda, `var idade = 17;` depois não aceita `idade = "dezessete";` dá erro compilação igual `int` daria.

Quando usar: quando o tipo já é óbvio pelo contexto (`var nome = "Arthur";`). Quando evitar: quando o tipo não fica claro só de olhar a linha, ai `var` prejudica qem lê o código depois.

### Escopo

Uma variável só existe dentro do bloco `{}` onde foi declarada:

```Csharp
   if (true)
   {
    int x = 10;
   } 
   Console.WriteLine(x); // ERRO: x não existe aqui fora
```

---

## 3. Conversão de tipos

### Conversão implícita

Acontece automaticamente quando não há risco de perda de dado:

```Csharp
   int numero = 10;
   double numeroDecimal = numero; // int -> double, automático, sem perda 
```

Funciona nesse sentido (menor pro maior) porque todo `int` cabe dentro de um `double` sem perder informação.

### Conversão explícita (casting)

Quando **pode** haver perda de dado, o C# exige que você seja explícito, ele não faz por conta própria:

```Csharp
   double valor = 9.8;
   int valorInteiro = (int)valor; // 9 --- trunca, não arredonda 
```

### Parse

Converte `string` para tipo numérico.

```Csharp
   string text = "25";
   int numero = int.parse(texto); // 25 
```

Se a string não for um número válido (`"abc"`), `Parse` **lança uma exceção** e quebra o programa. Você ainda não estudou tratamento de exceção (isso vem na Etapa 8), por enquanto, isso é motivo pra usar `TryParse` sempre que ler input externo.

### TryParse

Versão segura do Parse, não quebra o programa se a conversão falhar:

```Csharp
   string texto - "abc";
   bool sucesso = int.TryParse(texto, out int numero);

   if (sucesso)
   {
    Console.WriteLine($"Convertido: {numero}");
   }
   else
   {
    Console.WriteLine("Não foi possível converter.");
   } 
```

`out` aqui é uma palavra-chave que permite o método "retornar" um segundo valor (`numero`) além do `bool` principal. Você vai estudar `out` a fundo na Etapa 5 (métodos), por enquanto, trate como sintaxe fixa desse padrão.

**Regra prática que vale momorizar:** `Parse` quado você tem certeza absoluta que o valor é válido (ex: valor fixo no código). `TryParse` sempre que o valor vem de fora (input do usuário,arquivo, API), porque você não controla o que a pessoa digita.

---

## Checklist antes de ir pros exercícios

- [ ] Eu sei explicar por que `double` é usado para decimais em geral, mas `decimal` é obrigatório para dinheiro?
- [ ] Eu sei dizer, sem ambiguidade, quando uma divisão entre dois `int` trunca o resultado, mesmo que o destino final seja `double`?
- [ ] Eu sei a diferença prática entre `Parse`e `TryParse`, e sei dizer quando usar cada um?
- [ ] Eu sei o que acontece se eu tentar ler uma variável declarada, mas não inicializada?

---

## Exercícios

Todos os exercícios abaixo devem ler valores do usuário via `Console.ReadLine()` (que retorna `string`) e converter usando `Parse` ou `TryParse`, não deixe valores fixos no código como na Etapa 1.

1. **IMC**: lei peso (kg) e altura (m), calcule IMC (`peso / (altura * altura)`) imprima o resultado.
2. **Média Escolar**: Leia 3 notas, calcule e imprima a média.
3. **Conversor de unidades**: Leia um valor em metros, imprima convertido para centímetros e para quilômetros
5. **Cálculo de salário**: leia salário-base e hora extra trabalhadas, calcule salário final considerando hora extra a 50% a mais do valor da hora normal (você vai precisar definir quantas horas tem o mês-base, deixe isso como constante)

**Atenção**: Em pelo menos 2 exercícios (sua escolha), use `TryParse` explicitamente e trate o caso de entrada inválida imprimindo uma mensagem de erro, não deixe o programa quebrar silenciosamente.