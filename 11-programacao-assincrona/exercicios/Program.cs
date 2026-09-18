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

using System;
namespace Project;

using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

public class Program
{
    public static async Task<string> BaixarDados(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            string dados = await client.GetStringAsync(url);
            return dados;
        }
    }

    public static void Main()
    {
        
    }
}

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

// namespace Project
// {
    

//     public class Program
//     {
//         public static async Task<string> BaixarDados(string url, CancellationToken token)
//         {
//             using (HttpClient client = new HttpClient())
//             {
//                 string dados = await client.GetStringAsync(url);

//                 await Task.Delay(10000, token);
//                 return dados;
//             }
//         }

//         public static async Task Main()
//         {

//         using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

//             try
//             {
//                 var inicio = DateTime.Now;

//                 Task<string> tarefa1 = BaixarDados("https://jsonplaceholder.typicode.com/posts/1", cts.Token);
//                 Task<string> tarefa2 = BaixarDados("https://jsonplaceholder.typicode.com/posts/2", cts.Token);
//                 Task<string> tarefa3 = BaixarDados("https://jsonplaceholder.typicode.com/posts/3", cts.Token);
//                 Task<string> tarefa4 = BaixarDados("https://jsonplaceholder.typicode.com/posts/4", cts.Token);

//                 string[] resultados = await Task.WhenAll(tarefa1, tarefa2, tarefa3, tarefa4);

//                 var fim = DateTime.Now;

//                 foreach(string resultado in resultados)
//                 {
//                     Console.WriteLine(resultado + "\n\r");
//                 }

//                 Console.WriteLine($"Tempo gasto: {(fim - inicio).TotalSeconds}s");

//             }
//             catch (OperationCanceledException)
//             {
//                 Console.WriteLine("Timeout de 5 segundos atingido. Conexões fechadas.");
//             }

//         }


//     }
// }

// Exercício 6

// using System.Net.Http;
// using System.Text.Json;

// public class Program
// {
//     public async static Task Main()
//     {
//         string url = "https://jsonplaceholder.typicode.com/posts/1";
//         string caminho = "conteudo.json";

//         using HttpClient client = new HttpClient();

//         string conteudo = await client.GetStringAsync(url);

//         await File.WriteAllTextAsync(caminho, conteudo);

//         string conteudoLido = await File.ReadAllTextAsync(caminho);

//         Console.WriteLine("Conteúdo lido com sucesso! \n\r");
//         Console.WriteLine(conteudoLido);
//     } 
// }

// Exercício 7

// using System.Text.Json;
// using System.Net.Http;

// public class Program
// {
//     public async static Task<int> TamanhoDoConteudo(string url)
//     {
//         try
//         {
//             using HttpClient client = new HttpClient();

//             string conteudo = await client.GetStringAsync(url);

//             Console.WriteLine($"Tamanho de {conteudo}: {conteudo.Length}");
//             return conteudo.Length;
//         }
//         catch (Exception)
//         {
//             Console.WriteLine($"{url} falhou.");
//             return 0;
//         }
//     }
//     public async static Task Main()
//     {
//         var urls = new List<string> {
//             "https://jsonplaceholder.typicode.com/posts/1",
//         "https://jsonplaceholder.typicode.com/posts/2",
//         "https://jsonplaceholder.typicode.com/posts/3",
//         "https://jsonplaceholder.typicode.com/posts/4",
//         "https://jsonplaceholder.typicode.com/posts/5",
//         "https://jsonplaceholder.typicode.com/posts/6",
//         "https://jsonplaceholder.typicode.com/posts/7",
//         "https://jsonplaceholder.typicode.com/posts/8",
//         "https://jsonplaceholder.typicode.com/posts/9",
//         "https://jsonplaceholder.typicode.com/posts/10",
//         };

//         const int maxParalelo = 2;

//         for (int i = 0; i < urls.Count; i += maxParalelo)
//         {
//             var lote = urls.Skip(i).Take(maxParalelo);
//             var tarefas = lote.Select(url => TamanhoDoConteudo(url));

//             int[] tamanho = await Task.WhenAll(tarefas);
            
//             Console.WriteLine($"Lote completado \n\r");
//         }
//     }
// }

// Exercício 8

// public class Program
// {
//     public async static Task<string> SimulacaoLenta(string url, CancellationToken token, int segundos)
//     {
//         int tempo = segundos * 1000;
//         try
//         {
//             using HttpClient client = new HttpClient();

//             string conteudo = await client.GetStringAsync(url);
//             await Task.Delay(tempo, token);
//             Console.WriteLine("Conteúdo baixado!");
//             return "Sucesso";
//         }
//         catch (OperationCanceledException)
//         {
//             Console.WriteLine("Tempo excedido!");
//             return "TIMEOUT";
//         }
//     }
//     public async static Task Main()
//     {

//         Console.WriteLine("TESTE 1 - Deve retornar 'Conteúdo baixado!'");

//         string url1 = "https://jsonplaceholder.typicode.com/posts/1";
//         string url2 = "https://jsonplaceholder.typicode.com/posts/2";

//         var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

//         Task<string> tarefa1 = SimulacaoLenta(url1, cts.Token, 1);
        
//         await tarefa1;

//          var cts2 = new CancellationTokenSource(TimeSpan.FromSeconds(2));

//         Console.WriteLine("TESTE 2 - Deve retornar 'Tempo excedido!'");

//         Task<string> tarefa2 = SimulacaoLenta(url2, cts2.Token, 5);

//          await tarefa2;

//     }
// }

// DEBUG

//  public class Program
// {
//     async static Task<string> BaixarDados(string url)
// {
//     using (HttpClient client = new HttpClient())
//     {
//         string dados = await client.GetStringAsync(url);
//         return dados;
//     }
// }
 
// async static Task Main()
// {
//     Task tarefa = Task.Run(async () =>
//     {
//         string url = "https://api.example.com";
//         await Task.Delay(2000);
//         await BaixarDados(url);
//     });
 
//     await tarefa;
// }
// }

