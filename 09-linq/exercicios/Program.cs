// Exercício 1

// public class Email
// {
//     public static List<Email> cadastros = new List<Email>();
//     public string _Email { get; set; }
//     public string PassWord { get; set; }

//     public Email(string email, string passWord)
//     {
//         this._Email = email;
//         this.PassWord = passWord;
//     }

//     public static bool Cadastrar(string email, string passWord)
//     {
//         if (cadastros.Any(e => e._Email == email))
//             return false;

//         cadastros.Add(new Email(email, passWord));
//         return true;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Console.Write("Escreva o Email: ");
//         string ? email = Console.ReadLine();

//         Console.Write("Escreva a senha: ");
//         string ? PassWord = Console.ReadLine();

//         if (string.IsNullOrWhiteSpace(email))
//         {
//             Console.WriteLine("Email inválido!");
//         }
//         else if (string.IsNullOrWhiteSpace(PassWord))
//         {
//             Console.WriteLine("Senha inválido!");
//         }
//         else
//         {
//              if (Email.Cadastrar(email, PassWord) == true)
//             {
//                 Console.WriteLine("Cadastrado com sucesso!");
//             }
//             else
//             {
//                 Console.WriteLine("Erro: email já existe!");
//             }
//         }
//     }
// }

// Exercício 2

// public class Program
// {
//     delegate int Operacao(int a, int b);

//     static int Somar(int a, int b) => a + b;
//     static int Subtrair(int a, int b) => a - b;
//     static int Multiplicar(int a, int b) => a * b;

//     public static void Main()
//     {
//         int a, b, choice;
//         Operacao op;
//         Console.Write("Digite o valor 1: ");
//         if (!int.TryParse(Console.ReadLine(), out a))
//         {
//             Console.WriteLine("Valor de a inválido"); 
//             return;
//         }

//         Console.Write("Digite o valor 2: ");
//         if (!int.TryParse(Console.ReadLine(), out b))
//         {
//             Console.WriteLine("Valor de b inválido"); 
//             return;
//         }

//         Console.WriteLine("Escolha a sua operação: \n 1 - Soma \n 2 - Subtração \n 3 - Multiplicação");
//         int.TryParse(Console.ReadLine(), out choice);

//         switch (choice)
//         {
//             case 1:
//                 op = Somar;
//                 Console.WriteLine(op(a, b));
//                 break;

//             case 2:
//                 op = Subtrair;
//                 Console.WriteLine(op(a, b));
//                 break;

//             case 3:
//                 op = Multiplicar;
//                 Console.WriteLine(op(a, b));
//                 break;

//             default:
//                 Console.WriteLine("Opção inválida!");
//                 break;
//         }

//     }
// }

// Exercício 3

// public class Pedido
// {
//     public event Action<string>? StatusAlterado;

//     public void AtualizarStatus(string novoStatus)
//     {
//         Console.WriteLine("Status atualizado!");
//         StatusAlterado?.Invoke(novoStatus);
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Pedido pedido = new Pedido();

//         pedido.StatusAlterado += status => Console.WriteLine($"Grupo 1 receber o Status: {status}");
//         pedido.StatusAlterado += status => Console.WriteLine($"Grupo 2 recebeu o status: {status}");

//         pedido.AtualizarStatus("Status atualizado!");
//     }
// }

// Exercício 3.5

// public class Sensor
// {
//     public double Temperatura { get; set; }

//     public Sensor(double temperatura)
//     {
//         this.Temperatura = temperatura;
//     }

//     public event Action<double>? TemperaturaCritica;

//     public void AtualizarTemperatura (double temperaturaNova)
//     {
//         this.Temperatura = temperaturaNova;
//          Console.WriteLine("Temperatura atualizada!");

//         if (temperaturaNova > 100)
//         {
//            TemperaturaCritica?.Invoke(temperaturaNova);
//         }
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Sensor sensor = new Sensor(50.5);

//         sensor.TemperaturaCritica += temperatura => Console.WriteLine($"Bombeiros: Temperatura elevada: {temperatura}");

//         sensor.TemperaturaCritica += temperatura => Console.WriteLine($"Secretaria do bem-estar: Temperatura elevada: {temperatura}");
//         sensor.AtualizarTemperatura(99);
//     }
// }

// Exercício 4

namespace Project;
public class Produto
 {
     private decimal preco;
     public string Nome { get; set; }
     public int Quantidade { get; set; }

    public Produto(string nome, int quantidade, decimal Preco)
     {
         this.Nome = nome;
         this.Preco = Preco;
         this.Quantidade = quantidade;
     }

     public decimal Preco
     {
         get { return preco; }
         set
         {
             if (value <= 0) throw new ArgumentException("Preço inválido.");
            preco = value;
         }
     }

     public decimal ValorTotal()
     {
         return preco * Quantidade;
     }
 }

 public class Program
{
    public static void Main()
    {
        List<Produto> produtos = new List<Produto>();

        Produto mouse = new Produto("Mouse", 2, 300);
        Produto teclado = new Produto("Teclado", 5, 1000);
        Produto cadeira = new Produto("Cadeira", 1, 500);
        Produto monitor = new Produto("Monitor", 1, 5000);
        Produto gabinete = new Produto("Gabinete", 1, 900);

        produtos.Add(mouse);
        produtos.Add(teclado);
        produtos.Add(cadeira);
        produtos.Add(monitor);
        produtos.Add(gabinete);

        List<string> produtosCaros = produtos
            .Where(n => n.Preco >= 1000)
            .Select(n => n.Nome)
            .ToList();

        foreach(string nome in produtosCaros)
        {
            Console.WriteLine($"Nome do produto: {nome}");
        }

    }
}

public class FiltroProdutos
{
    public static List<string> NomesDosProdutosCaros(List<Produto> produtos, decimal precoMinimo)
    {
        return produtos
            .Where(p => p.Preco >= precoMinimo)
            .Select(p => p.Nome)
            .ToList();
    }
}

// Exercício 5

// public class Venda
// {
//     public string Vendedor { get; set; }
//     public decimal Valor { get; set; }

//     public Venda (string vendedor, decimal valor)
//     {
//         this.Vendedor = vendedor;
//         this.Valor = valor;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         List<Venda> vendas = new List<Venda>();

//         Venda venda1 = new Venda("Carlos", 1500);
//         Venda venda2 = new Venda("Antônio", 3000);
//         Venda venda3 = new Venda("Carlos", 1000);
//         Venda venda4 = new Venda("Antônio", 500);
//         Venda venda5 = new Venda("Carlos", 5000);

//         vendas.Add(venda1);
//         vendas.Add(venda2);
//         vendas.Add(venda3);
//         vendas.Add(venda4);
//         vendas.Add(venda5);

//       var vendasPorVendedor = vendas.GroupBy(n => n.Vendedor);

//       foreach(var grupo in vendasPorVendedor)
//         {
//             var total = grupo.Sum(n => n.Valor);
//             Console.WriteLine($"Vendedor: {grupo.Key} Valor total de vendas: {total}");
//         }
//     }
// }

// Exercício 6

// public class Pessoa
// {
//     public string Name { get; set; }

//     public Pessoa (string name)
//     {
//         this.Name = name;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         List<Pessoa> pessoas = new List<Pessoa>();

//         Pessoa pessoa1 = new Pessoa("Arthur");
//         Pessoa pessoa2 = new Pessoa("Joao");
//         Pessoa pessoa3 = new Pessoa("Julio");
//         Pessoa pessoa4 = new Pessoa("Diniz");
//         Pessoa pessoa5 = new Pessoa("Pablo");
//         Pessoa pessoa6 = new Pessoa("Carol");

//         pessoas.Add(pessoa1);
//         pessoas.Add(pessoa2);
//         pessoas.Add(pessoa3);
//         pessoas.Add(pessoa4);
//         pessoas.Add(pessoa5);
//         pessoas.Add(pessoa6);

//         Pessoa? pessoa = pessoas.FirstOrDefault(n => n.Name == "Arthur");

//         if (pessoa == null)
//         {
//             Console.WriteLine("Pessoa não encontrada.");
//         }
//         else
//         {
//             Console.WriteLine($"Nome da pessoa: {pessoa.Name}");
//         }

//         Pessoa? pessoaTeste = pessoas.FirstOrDefault(n => n.Name == "Pedro");

//         if (pessoaTeste == null)
//         {
//             Console.WriteLine("Pessoa não encontrada.");
//         }
//         else
//         {
//             Console.WriteLine($"Nome da pessoa: {pessoaTeste.Name}");
//         }
//     }
// }

// Exercício 7

// List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 13, 50, 100, 45, 98, 56, 67, 42 };

// var soma = numeros.Sum();
// double media = numeros.Average();
// var maximo = numeros.Max();
// var minimo = numeros.Min();
// var quantidadeDePares = numeros.Count(n => n % 2 == 0);

// Console.WriteLine($"Soma: {soma} \n Média: {media} \n Máximo: {maximo} \n Mínimo: {minimo} \n Quantidade de numeros pares: {quantidadeDePares}");

// DEBUG

// class Produto
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

// public class Program
// {
//     public static void Main()
//     {
//         List<Produto> produtos = new List<Produto>
//         {
//             new Produto("Caneta", 5, 2.50m),
//             new Produto("Caderno", 10, 15.00m)
//         };
 
//         var produtoBuscado = produtos.FirstOrDefault(p => p.Nome == "Lápis");

//         if (produtoBuscado == null)
//         {
//             Console.WriteLine("Produto não encontrado.");     
//         }
//         else
//         {
//             Console.WriteLine($"Preço encontrado: {produtoBuscado.Preco}");
//         }
 
//         List<Func<int>> funcoes = new List<Func<int>>();
 
//         for (int i = 0; i < 3; i++)
//         {
//             int j = i;
//             funcoes.Add(() => j);
//         }
 
//         foreach (var funcao in funcoes)
//         {
//             Console.WriteLine(funcao()); // o que você espera imprimir aqui, e o que realmente imprime?
//         }
//     }
// }