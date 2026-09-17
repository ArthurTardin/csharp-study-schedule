// Exercício 1

// public class Program
// {
//     public static async Task Main()
//     {
//         async Task SimularDownload(string nomeArquivo, int segundos)
//         {
//             Console.WriteLine($"Começando download de {nomeArquivo}");
//             await Task.Delay(segundos * 1000);
//             Console.WriteLine($"{nomeArquivo} completou em {segundos}s");
//         } 

//          var incio = DateTime.Now;

//         Task tarefa1 = SimularDownload("Arquivo1.zip", 1);
//         Task tarefa2 = SimularDownload("Arquivo2.zip", 2);
//         Task tarefa3 = SimularDownload("Arquivo3.zip", 3);

//         await Task.WhenAll(tarefa1, tarefa2, tarefa3);

//          var fim = DateTime.Now;
//         Console.WriteLine($"Total levou: {(fim - incio).TotalSeconds} segundos");
//     }
// }

// Exercício 2

// using System.Net.Http;

// var client = new HttpClient();

// string resposta = await client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/1");

// Console.WriteLine(resposta);

// Eercício 3

// async Task ProcessarNumeros(List<int> numeros, int maxParalelo)
// {
//     for (int i = 0; i < numeros.Count; i += maxParalelo)
//     {
//         var lote = numeros.Skip(i).Take(maxParalelo);

//         var tarefas = lote.Select(async numero =>
//         {
//            Console.WriteLine($"Processando {numero}");
//            await Task.Delay(1000);
//            Console.WriteLine($"{numero} Concluído"); 
//         });

//         await Task.WhenAll(tarefas);
//         Console.WriteLine($"Lote completado\n");
//     }
// }

// var numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
// await ProcessarNumeros(numeros, 3);

// Exercício 4

// async Task SimularDownloadComCancelamento (CancellationToken token)
// {
//     try
//     {
//         Console.WriteLine("Download começando...");

//         for (int i = 0; i < 5; i++)
//         {
//             token.ThrowIfCancellationRequested();

//             Console.WriteLine($"Progresso: {i + 1}/5");
//             await Task.Delay(1000);
//         }

//         Console.WriteLine("Download completo!");
//     }
//     catch (OperationCanceledException)
//     {
//         Console.WriteLine("Download foi cancelado!");
//     }
// }

// var cts = new CancellationTokenSource();
// Task tarefa = SimularDownloadComCancelamento(cts.Token);

// await Task.Delay(1000);
// cts.Cancel();

// await tarefa;

// Exercício 5

public class Program
{
    public static async Task<string> BaixarDados(string url, CancellationToken token)
    {
        using (HttpClient client = new HttpClient())
        {
            string dados = await client.GetStringAsync(url);

            await Task.Delay(10000, token);
            return dados;
        }
    }

    public static async Task Main()
    {

       using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        try
        {
            var inicio = DateTime.Now;

            Task<string> tarefa1 = BaixarDados("https://jsonplaceholder.typicode.com/posts/1", cts.Token);
            Task<string> tarefa2 = BaixarDados("https://jsonplaceholder.typicode.com/posts/2", cts.Token);
            Task<string> tarefa3 = BaixarDados("https://jsonplaceholder.typicode.com/posts/3", cts.Token);
            Task<string> tarefa4 = BaixarDados("https://jsonplaceholder.typicode.com/posts/4", cts.Token);

            string[] resultados = await Task.WhenAll(tarefa1, tarefa2, tarefa3, tarefa4);

            var fim = DateTime.Now;

            foreach(string resultado in resultados)
            {
                Console.WriteLine(resultado + "\n\r");
            }

            Console.WriteLine($"Tempo gasto: {(fim - inicio).TotalSeconds}s");

        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Timeout de 5 segundos atingido. Conexões fechadas.");
        }

    }


}

