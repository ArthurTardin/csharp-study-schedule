// // Exercício 1

// List<string> compras = new List<string>();
// compras.Add("Arroz");
// compras.Add("Feijão");
// compras.Add("Batata");
// compras.Add("Carne");
// compras.Add("Salada");

// compras.Remove("Arroz");

// foreach (var compra in compras)
// {
//     Console.WriteLine(compra);
// }

// int total = compras.Count;

// Console.WriteLine($"total de compras: {total}");

// // Exercício 2

// Dictionary<string, string> contatos = new Dictionary<string, string>();

// contatos.Add("Arthur", "19999999");
// contatos.Add("Caua", "129999999");
// contatos.Add("Roberto", "551122999");

// if (contatos.ContainsKey("Caua"))
// {
//     Console.WriteLine(contatos["Caua"]);
// }
// else
// {
//     Console.WriteLine("Contato não encontrado.");
// }

// // Exercício 3

// Queue<string> filaAtendimento = new Queue<string>();

// filaAtendimento.Enqueue("Joao");
// filaAtendimento.Enqueue("Carlos");
// filaAtendimento.Enqueue("Pedro");
// filaAtendimento.Enqueue("Felipe");

// filaAtendimento.Dequeue();
// filaAtendimento.Dequeue();

// foreach (var pessoa in filaAtendimento)
// {
//     Console.WriteLine(pessoa);
// }

// // Exercício 4

// Stack<string> historico = new Stack<string>();

// historico.Push("Página 1");
// historico.Push("Página 2");
// historico.Push("Página 3");
// historico.Push("Página 4");

// historico.Pop();
// historico.Pop();

// foreach (var pagina in historico)
// {
//     Console.WriteLine(pagina);
// }

List<string> lista = new List<string>();
bool sair = false;

while (!sair)
{
    Console.WriteLine("\n1 - Adicionar");
    Console.WriteLine("2 - Remover");
    Console.WriteLine("3 - Ver tudo");
    Console.WriteLine("4 - Sair");
    Console.Write("Escolha uma opção: ");
    
    int.TryParse(Console.ReadLine(), out int escolha);

    switch (escolha)
    {
        case 1:
            Console.Write("Digite o nome do que você quer adicionar: ");
            string entradaAdicionar = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(entradaAdicionar))
            {
                Console.WriteLine("Digite um valor válido!");
                break;
            }

            lista.Add(entradaAdicionar);
            Console.WriteLine("Adicionado com sucesso!");
            break;

        case 2:
            Console.Write("Digite o nome do que você deseja remover: ");
            string entradaRemover = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(entradaRemover) || !lista.Contains(entradaRemover))
            {
                Console.WriteLine("Esse produto não existe ou o nome é inválido!");
                break;
            }

            lista.Remove(entradaRemover);
            Console.WriteLine("Removido com sucesso!");
            break;

        case 3:
            if (lista.Count == 0)
            {
                Console.WriteLine("A lista está vazia!");
                break;
            }

            foreach (var item in lista)
            {
                Console.WriteLine($"item: {item}");
            }
            break;

        case 4:
            Console.WriteLine("Saindo...");
            sair = true;
            break;

        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}