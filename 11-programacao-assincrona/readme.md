# Etapa 11 - Programação Assíncrona

## 1. Threads e o conceitos de "simultaneidade"

Uma **thread** é um "caminho de execução" dentro do seu programa, um "fio" de código rodando do começo ao fim. Por padrão, seu programa tem **uma thread**, a thread principal.

```CSharp
    Console.WriteLine("Antes");
    Thread.Sleep(2000); //bloqueia a thread por 2 segundos
    Console.WriteLine("Depois");
```

Isso imprime "Antes", fica travado por 2 segundos (bloqueando tudo, nada mais acontece), depois imprime "Depois", é síncrono puro. Se o Sleep fosse uma operação de rede real (baixar um arquivo), sua interface toda congelaria por aqueles 2 segundos.

Você **nunca** vai usar `new Thread()` direto no código moderno de C#, isso é a velha forma. A forma moderna é `Task`, que você vai aprender aqui.

---

## 2. Task e Task<T> - Abstraindo threads com segurança

Uma `Task` representa um trabalho um trabalho que vai rodar, possivelmente em parelelo, e retornar um resultado depois.

```CSharp
    Task tarefa = Task.Run(() =>
    {
        Thread.Sleep(2000);
        Console.WriteLine("Tarefas completou");
    });

    Console.WriteLine("Linha main");
    tarefa.Wait(); // espera a tarefa terminar
```

Aqui, `Task.Run` começa a tarefa (possivelmente em outra thread), e seu programa continua (imprime "Linha main"). Depois, `tarefa.Wait()` bloqueia e espera ela terminar.

### Com retorno:

```CSharp
    Task<int> tarefa = Task.Run(() =>
    {
        return 2 + 2;
    });

    int resultado = tarefa.Result; // espera e pega o resultado
    Console.WriteLine(resultado); // 4
```

**Diferença importante**: `Wait()` espera a tarefa terminar, mas **descarta** o resultado. `Result` espera E retorna o resultado. Ambos **bloqueiam**, por isso existe `async`/`await`, que fazer a mesma espera sem bloquear a thread.

## 3. async e await - non-blocking wait

```CSharp
    async Task Main()
    {
        Console.WriteLine("Antes");
        await Task.Delay(2000) // espera 2 segundos, SEM BLOQUEAR
        Console.WriteLine("Depois");
    }
```

`await` significa "espere isso terminar, mas deixe outras coisas acontecerem enquanto isso". Diferente de `Thread.Sleep(2000)` que **congela tudo**, `await Task.Delay(2000)` suspende essa função e deixa a thread livre parac fazer outra coisa.

**Regra de ouro:** `async` marca um método que **pode usar `await` dentro dele**. `await` só pode aparecer dentro de um método `async`.

```Csharp
    async Task<string> BaixarDados(string url)
    {
        // HttpClient é uma classe pra fazer requisições HTTP
        using (HttpClient client = new HttpClient())
        {
            string dados = await client.GetStringAsync(url); // espera, sem bloquear
            return dados;
        }
    }

    async Task Main()
    {
        string resultado = await BaixarDados("https://api.example.com/dados");
        Console.WriteLine(resultado);
    }
```
A cadeia é importante: se você chama um método `async`, você precisa fazer `await` para esperar ele. Se faz `await`, sem método também precisa ser `async`. Essa "contaminação" assíncrona sobre até a raiz.

---

## 4. Executando múltiplas tarefas em paralelismo

```CSharp
    async Task Main()
    {
        Task<string> tarefa1 = BaixarDados("url1");
        Task<string> tarefa2 = BaixarDados("url2");
        Task<string> tarefa3 = BaixarDados("url3");

        // começa as 3 saimultaneamente, depois espera todas terminarem
        string[] resultados = await Task.WhenAll(tarefa1, tarefa2, tarefa3);

        foreach (string resultado in resultados)
        {
            Console.WriteLine(resultado);
        }
    }
```

`Task.WhenAll` espera **todas** as tarefas terminarem. Se um levar 1s, outra 2s outra 3s, `WhenAll` retorna só quando a masi lenta (3s) terminou, economizando tempo em relação a esperar uma por uma (que seria 1+2+3=6s)

`Task.WhenAny` retorna assim que **a primeira** terminar:

```CSharp
    Task<string> primeira = await Task.WhenAny(tarefa1, tarefa2, tarefa3);
```

---

## 5. Armadilhas clássicas de código assíncrono

async void - NÃO USE (a menos que seja event handler)

```Csharp
    async void Tarefa() // ERRADO
    {
        await Task.Delay(1000);
        Console.WriteLine("Pronto");
    }

    Tarefa(); // começa a tarefa, mas você não consegue saber quando termina
```

`async void` é "fogo e esquece", você não consegue esperar, não consegue saber se completou, não consegue tratar exceção facilmente. Exceções lançadas dentro dela morrem silenciosamente. **Nunca use `async void` a menos que seja um event handler** (ex: `Button.Click`).

Use `async Task` (sem retorno) ou `async Task<T>` (com retorno) em vez disso.

### .Result bloqueante - cria deadlock fácil

```CSharp
    Task<int> tarefa = MetodoAsync();
    int resultado = tarefa.Result; // BLOQUEIA aqui, esperando
```

Se `MetodoAsync()` depender de contexto síncrono (o que raramente acontece em console, mas comum em UI), isso pode criar **deadlock**, a tarefa espera pelo contexto, e o contexto está bloqueado esperando a tarefa. use `await` em vez:

```CSharp
    int resultado = await tarefa;
```

### Deadlock por contexto de sincronização

Isso é avançado demias para esse etapa, mas registra: em ASP.NET ou Windows Forms `.Result` sem cuidado pode travar porque o código espera um contexto que está bloqueado. Console é mais seguro, mas o hábito ruim paga.

---

## 6. Operações assíncronas comuns

### HttpClient = requisições HTTP

```CSharp
    using (HttpClient client = new HttpClient())
    {
        string json = await client.GetStringAsync("https://api.github.com/users/github");
        Console.WriteLine(json);
    }
```

### Arquivo Assíncrono

```CSharp
    string conteudo = await File.ReadAllTextAsync("arquivo.txt");
    await File.WriteAllTextAsync("saida.txt", conteudo);
```

Tudo que você fez na Etapa 10 com `File.ReadAllText` tem versão `async`: `ReadAllTextAsync`, etc.

## 7. CancellationToken - interromper uma tarefa

```Csharp
    async Task BaixarArquivo(string url, CancellationToken token)
    {
        using (HttpClient client = new HttpClient())
        {
            var reposta = await client.GetAsync(url, token);
            // se token.Cancel() for chamado enquanto espera, lança OperationCanceledException
        }
    }

    var cts = new CancellationTokenSource();
    Task tarefa = BaixarArquivo("url", cts.Token);

    // depois,  se quiser imterromper:
    cts.Cancel();

    try
    {
        await tarefa;
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Download cancelado");
    }
```

usado quando você quer permitir interrupção elegante, ex: usuário clica "Cancelar Download", você chama `cts.Cancel()`, e a tarefa para.

---

## Checklist antes de ir pro exercícios

- [ ] Eu sei explicar a diferença entre `Thread.Sleep(2000)` (bloqueia) e `await Task.Delay(2000)` (não bloqueia)?
- [ ] Eu sei por quê `async void` é uma armadilha, e qual é a alternativa correta?
- [ ] Eu sei por quê `.Result` pode criar deadlock, e quando usar `await` em vez disso?
- [ ] Eu sei quando usar `Task.WhenAll` (espera tudo) vs `Task.WhenAny` (espera primeira)?

---

## Exercícios

1. **Simulador de download múltiplo**: crie um método assíncrono que simula download de 3 arquivos (cada um com um `Task.Delay` diferente, ex: 1s, 2s, 3s). Use `Task.WhenAll` pra esperar todos terminarem, e imprima quanto tempo levou no total (deveria ser ~3s, o tempo do mais lento, não 6s)
2. **Consumo de API pública com HttpClient**: requisição GET assíncrona pra `https://jsonplaceholder.typicode.com/posts/1` (um endpoint fake público gratuito), imprima o JSON retornado
3. **Processamento paralelo de tarefas**: crie um método assíncrono que recebe uma lista de números e, pra cada número, faz uma "operação lenta" (simule com `Task.Delay`), processando até 3 números em paralelo (use `Task.WhenAll` com batches)
4. **Cancelamento de tarefa**: simulador de download que aceita `CancellationToken`, e no `Main`, depois de 1 segundo, chama `.Cancel()` pra interromper o download

### [DEBUG]
Código abaixo com dois bugs: `async void` usado incorretamente, e `.Result` bloqueante causando espera desnecessária (pode causar deadlock em contextos reais). Ache e corrija ambos.
 
```csharp
async void BaixarDados(string url)
{
    using (HttpClient client = new HttpClient())
    {
        string dados = await client.GetStringAsync(url);
        Console.WriteLine(dados);
    }
}
 
async Task Main()
{
    Task tarefa = Task.Run(async () =>
    {
        await Task.Delay(2000);
        BaixarDados("https://api.example.com");
    });
 
    string resultado = tarefa.Result; // bug aqui -- bloqueante
}
```

### [TESTE]
Escreva testes assíncronos (xUnit com `[Fact]` async) que testem o método de requisição HTTP: teste que uma requisição com URL válida retorna dados não-vazio, e teste que uma URL inválida lança exceção específica (use mock de `HttpClient` com um substitute/fake, ou um WireMock local se tiver familiaridade).