// exercício 1
// using System.IO;

// public class Program
// {
//     public static void Main()
//     {
//         int choice;
//         bool rodando = true;
//         while (rodando)
//         {
//             Console.WriteLine("Diario \n Escolha uma opção: \n 1 - Escrever \n 2 - Ler");

//             if (!int.TryParse(Console.ReadLine(), out choice))
//             {
//                 Console.WriteLine("Valor inválido, digite novamente!");
//             }
//             else
//             {
//                 switch (choice)
//                 {
//                     case 1:
//                         Console.WriteLine("Digite o que deseja adicionar: ");
//                         string texto = Console.ReadLine()!;

//                         if (string.IsNullOrWhiteSpace(texto))
//                         {
//                             Console.WriteLine("Texto inválido!");
//                             rodando = false;
//                             break;
//                         }
//                         else
//                         {
//                             File.AppendAllText("diario.txt", texto + Environment.NewLine);
//                             rodando = false;
//                         }
//                         break;

//                     case 2:
//                         string conteudo;
//                         Console.WriteLine("Conteudo dentro do arquivo:");
//                         try
//                         {
//                              conteudo = File.ReadAllText("diario.txt");
//                              Console.WriteLine(conteudo);
//                         }
//                         catch (FileNotFoundException ex)
//                         {
//                             Console.WriteLine($"Erro: {ex.Message}");
//                         }
//                         rodando = false;
//                         break;
//                     default:
//                         Console.WriteLine("Opção inválida!");
//                         break;
//                 }


//             }
//         }
//     }   
// }

// Exercício 2

// using System.IO;

// public class Produto
//  {
//      private decimal preco;
//      public string Nome { get; set; }
//      public int Quantidade { get; set; }

//     public Produto(string nome, int quantidade, decimal Preco)
//      {
//          this.Nome = nome;
//          this.Preco = Preco;
//          this.Quantidade = quantidade;
//      }

//      public decimal Preco
//      {
//          get { return preco; }
//          set
//          {
//              if (value <= 0) throw new ArgumentException("Preço inválido.");
//             preco = value;
//          }
//      }

//      public decimal ValorTotal()
//      {
//          return preco * Quantidade;
//      }
//  }

//  public class Program
// {
//     public static void Main()
//     {
//         List<Produto> produtos = new List<Produto>();

//         Produto produto1 = new Produto("mouse", 2, 500);
//         Produto produto2 = new Produto("Teclado", 1, 500);
//         Produto produto3 = new Produto("Monitor", 1, 1000);
//         Produto produto4 = new Produto("Cadeira", 1, 900);
//         Produto produto5 = new Produto("Mesa", 2, 1000);

//         produtos.Add(produto1);
//         produtos.Add(produto2);
//         produtos.Add(produto3);
//         produtos.Add(produto4);
//         produtos.Add(produto5);

//         IEnumerable<string> LinhaCSV = produtos.Select(p => $"{p.Nome},{p.Preco},{p.Quantidade}");

//         File.WriteAllLines("produtos.csv", LinhaCSV);
//     }
// }

// Exercício 3

// using System.Text.Json;

// public class Pessoa
// {
//     public string Nome { get; set; } = "";
//     public int Idade { get; set; }

//     public Pessoa(string nome, int idade)
//     {
//         this.Nome = nome;
//         this.Idade = idade;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         List<Pessoa> pessoas = new List<Pessoa>();

//         Pessoa pessoa1 = new Pessoa("Arthur", 17);
//         Pessoa pessoa2 = new Pessoa("Pablo", 16);
//         Pessoa pessoa3 = new Pessoa("Julio", 17);
//         Pessoa pessoa4 = new Pessoa("Diniz", 17);

//         pessoas.Add(pessoa1);
//         pessoas.Add(pessoa2);
//         pessoas.Add(pessoa3);
//         pessoas.Add(pessoa4);
        
//         string jsonLista = JsonSerializer.Serialize(pessoas);
//         File.WriteAllText("pessoas.json", jsonLista + Environment.NewLine);

//         string jsonListaLido = File.ReadAllText("pessoas.json");
//         List<Pessoa>? pessoasLidas = JsonSerializer.Deserialize<List<Pessoa>>(jsonListaLido);

//         if (pessoasLidas != null)
//         {
//            foreach(var pessoa in pessoasLidas)
//             {
//                 Console.WriteLine($"Nome: {pessoa.Nome} Idade: {pessoa.Idade} \n");
//             }
//         }
//         else
//         {
//             Console.WriteLine("Pessoa não encontrada!");
//         }
//     }
// }

// Exercício 4

// public class Metodo
// {
//     static public void LerArquivo(string arquivo)
//     {
//         try
//         {
//            string leitura = File.ReadAllText(arquivo);
//            Console.WriteLine(leitura);
//         }
//         catch (FileNotFoundException)
//         {
//             Console.WriteLine("Arquivo não encontrado!");
//             File.WriteAllText(arquivo, "Este é o conteúdo padrão do arquivo.");
//             Console.WriteLine("Arquivo criado com sucesso!");
//         }
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Metodo.LerArquivo("diario.txt");
//     }
// }

// debug

using System.Text.Json;

class Produto
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
}

public class Program
{
    public static void Main()
    {
        using (StreamWriter writer = new StreamWriter("produtos.txt"))
        {
            writer.WriteLine("Caneta - R$2.50");
        writer.WriteLine("Caderno - R$15.00");
        }
        
        

        try
        {
            string jsonIncompleto = "{\"Preco\": 55 }";
        Produto? produto = JsonSerializer.Deserialize<Produto>(jsonIncompleto);
        Console.WriteLine($"Preço do produto: {produto.Preco}");
        }
        catch(JsonException)
        {
            Console.WriteLine($"Erro: Json mal formatado");
        }
    }
} 
