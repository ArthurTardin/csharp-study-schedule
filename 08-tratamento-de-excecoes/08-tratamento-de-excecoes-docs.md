# Etapa 8 - Tratamento de Exceções

## 1. Por que tratar exceção?

Você já viu, desde a Etapa 2, situações onde uma operação pode falhar em runtime, `int.Parse` numa string inválida, acessar índice fora do array, dividir por zero. Até agora, sua estratégia foi **evitar** esses erros com validação prévia (`TryParse`, `ContainsKey`, checar `Count` antes de acessar índice). Isso continua sendo a primeira linha de defesa, validar antes é sempre melhor que capturar depois.

Mas existem situações onde você **não pode prever ou evitar** o erro de antemão, ex: tentar ler um arquivo que o usuário pode ter apagado entre você checar que existia e você realmente abri-lo, ou uma chamada de rede que pode falahar por motivos fora do seu controle. Para esses casos, existe `try`/`catch`.

---

## 2. try, catch, finally

```CSharp
    try
    {
        int numero = int.Parse("abc") // isso lança FormatException
        Console.WriteLine(numero);
    }
    catch (FormatException ex)
    {
        Console.WriteLine($"Erro de formato {ex.Message}");
    }
```

- `try`: bloco onde você coloca o código que **pode** lançar exceção.
- `catch (TipoDaExcecao ex)`: captura uma exceção de um tipo específico, se ela ocorrer. `ex` é a variável que guarda os detalhes da exceção capturada (`ex.Message` é a mensagem descritiva, `ex.StackTrace` mostra onde ocorreu, útil para debug).
- Se nenhum exceção ocorrer, o `catch` **nunca executa**, o problema segue normalmente depois do bloco `try`.

### finally

```CSharp
   try
   {
        // código arriscado
   } 
   catch (Exception ex)
   {
        // trata o erro
   }
   finally
   {
        Console.WriteLine("Isso sempre executa, com erro ou sem erro.");
   }
```

`finally` executa **sempre**, teve exceção ou não, foi capturada ou não. Usado tipicamente para liberar recursos (fechar arquivo, fechar conexão de rede/banco) que precisam ser limpos independente do resultado. Você vai usar isso mais concretamente quando trabalhar com arquivos (Etapa 10).

## 3. Múltiplos catch - ordem importa

```CSharp
   try
   {
        int[] numeros = { 1, 2, 3 };
        Console.WriteLine(numero[10]);
   } 
   catch (IndexOutOfRangeException ex)
   {
        Console.WriteLine("Índice inválido: " + ex.Message);
   }
   catch (Exception ex)
   {
        Console.WriteLine("Erro genérico: " + ex.Message);
   }
```

Você pode ter vários blocos `catch`, cada um para um tipo diferente de exceção. O C# testa na ordem em que estão escritos, de cima para baixo, e usa o primeiro que combinar. Isso sgnifica: exceções mais específicas devem vir antes das mais genéricas. Se você colocar `catch (Exception ex)` primeiro, ele captura tudo, e os `catch` mais específico abaixo dele nunca são alcançados, na verdade isso é erro de compilação em C# (`catch` inalcançável), o que ajuda a te proteger dessa ordem errada.

`Exception` é a classe base de todas as exceções em C#, capturar `catch (Exception ex)` pega qualquer coisa. Isso é poderoso, mas perigoso: é fácil de abusar disso para "esconder" qualquer erro, sem realmente entender ou tratar a causa. Trate `catch (Exception)` genérico como último recurso, não como padrão, geralmente só faz sentido no nível mais alto do programa (ex: logar o erro antes de fechar), não espalhado por todo canto.

---

## 4. Exceções comuns que você já viu (agora formalizadas)

- `FormatException` - `Parse` falha por formato inválido.
- `IndexOutOfRangeException` - Acesso a índice inválido em array
- `ArgumentOutOfRangeException` - Acesso a índice inválido em List, ou argumento fora do intervalo esperado.
- `KeyNotFoundException` - Acesso a chave inexistente em Dictionary
- `NullReferenceException` - Tentativa de usar um objeto que é `null`
- `DivideByZeroException`- Divisão por zero (só com tipos inteiros, `double/decimal` tem comportamento deferente, gera `Infinity` ou `NaN` em vez de lançar exceção, no caso do `double`)
- `InvalidOperationException` - Operação chamada num estado inválido do objeto (você já usou isso na Etapa 6/7)

---

## 5. throw - lançando exceções (revisão formal)

Você já faz isso desde a Etapa 6:

```CSharp
   if (idade < 0)
   {
        throw new ArgumentException("Idade Inválida.");
   } 
```

Agora que você sabe capturar, entende o ciclo completo> quem lança (`throw`) não precisa saber quem vai capturar (`catch`), são camadas diferentes do código, possivelmente até os métodos/classes diferentes. Isso é o princípio de "Falhar alto, tratar embaixo", o método que detecta o problema lança a exceção, e quem chamou essa método (ou alguém mais acima na cadeira de chamadas) decide como reagir.

### Re-lançando (rethrow)

```CSharp
   catch (Exception ex)
   {
        Console.WriteLine("Logando o erro: " + ex.message);
        throw // relança a mesma exceção preservando o stack trace original
   } 
```

usado quando você quer fazer algo com a exceção (logar, por exemplo), mas aunda deixa ela se propagar para quem chamou, em vez de `engolir` o erro silenciosamente.

### Exceção customizada

```CSharp
    class SaldoInsuficienteException : Exception 
    {
        public SaldoInsuficienteException (string mensagem) : base(mensagem) { }
    }
```

Criar sua própria clsse de exceção, herdando o Exception (ou de uma exceção mais específica), permite comunicar erros de domínio específico do seu programa, em vez de usar só as exceções genéricas do .NET. Isso conecta direto com o que você já estudou na Etapa 7 - `SaldoInsuficienteException` é uma `Exception`, através de herança.

---

## Checklist antes de ir pros exercícios

- [X] Eu sei por que `catch (Exception ex)` genérico deve vir depois de catches mais específicos, nunca antes
- [X] Eu sei a diferença prática entre `catch` e `finally`, o que executa sempre e o que executa só condicionalmente?
- [X] Eu sei explicar por que "validar antes com TryParse" e "capturar depois com try/catch" não são a mesma estratégia, e quando cada uma faz mais sentido?
- [X] Eu sei criar uma exceção customizada herdando de `Exception`?

---

## Exercícios

1. Divisão segura: Método que recebe dois números e retorna a divisão, tratando `DivideByZeroException` (lembra: com `int`, divisão por zero lança exceção, com `double`, não, force o cenário com `int` para ver a exceção de verdade).
2. Leitura seguro de índice: dado um array fixo, peça um índice ao usuário e imprima o valor, tratando `IndexOutOfRangeException` com mensagem amigável em vez de o programa quebrar.
3. Sistema de login com exceção customizada: crie uma exceção `CredenciaisInvalidasException`, e um método `Login(string usuario, string senha)` que lança essa exceção customizada se as credenciais não baterem com um valor fixo esperado. Capture essa exceção no `Main` e mostre mensagem amigável.
4. Validador de idade com múltiplos catches: método que recebe uma string, converte para `int` (deixe isso lançar `FormatException` de propósito, sem TryParse desse vez, só para praticar captura) e depois valida se é uma idade válida (lançando `ArgumentException` se for negativa ou maior que 120). No `Main`, capture os dois tipos de exceção separadamente, com mensagem diferente para cada.

## [DEBUG]

Código abaixo com dois problemas: um `catch` genérico escondendo um bug real, e um uso indevido de `finally`. Ache e corrija, e explique por escrito por que o `catch (Exception)` ali está mascarando um problema, não resolvendo.

```CSharp
    static void ProcessarPedido(int quantidade, int estoqueDisponivel)
{
    try
    {
        int quantidadeRestante = estoqueDisponivel / quantidade; // bug proposital escondido aqui
        Console.WriteLine($"Processado. Restam {quantidadeRestante} no estoque.");
    }
    catch (Exception)
    {
        Console.WriteLine("Pedido processado com sucesso."); // isso está mentindo sobre o que aconteceu
    }
    finally
    {
        Console.WriteLine("Conexão com banco de dados encerrada."); // nunca foi aberta conexão nenhuma
    }
}

ProcessarPedido(10, 0); // o que acontece aqui?
```

## [TESTE]

Escreva testes xUnit cobrindo os caminhos de exceção do Exercício 3 (sistema de login): teste que credenciais corretas não lançam exceção (login bem-sucedido), e teste que credenciais incorretas lançam `CredenciaisInvalidasException` (usando `Assert.Throws`).