// Exercício 1

// public class Metodos
// {
//     public static int Divisao(int a, int b)
//     {
//         try
//         {
//             return a / b;
//         }
//         catch (DivideByZeroException ex)
//         {
//             Console.WriteLine($"Erro de divisão: {ex.Message}");
//             return 0;
//         }
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Console.WriteLine(Metodos.Divisao(10, 0));
//         Console.WriteLine(Metodos.Divisao(10, 2));
//     }
// }

// Exercício 2

// public class Metodo
// {
//     public static int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

//     public static bool IndicePedido(int indice)
//     {
//         try
//         {
//             Console.WriteLine($"Valor do índice: {numeros[indice]}");
//             return true;
//         }
//         catch (IndexOutOfRangeException ex)
//         {
//             Console.WriteLine($"Valor inválido: {ex.Message}");
//             return false;
//         }

//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         int indice;

//         Console.WriteLine("Escolha um índice de 0 - 9");
//         if (!int.TryParse(Console.ReadLine(), out indice))
//         {
//             Console.WriteLine("Valor inválido, digite apenas números");
//         }
//         else
//         {
//            Metodo.IndicePedido(indice);


//         }


//     }
// }

// Exercício 3

// namespace project;
// public class CredenciaisInvalidasException : Exception
// {
//     public CredenciaisInvalidasException() : base("Credenciais inválidas, tente novamente!") { }
// }
// public class Metodo
// {
//     public string Usuario { get; set; }
//     public string Senha { get; set; }

//     public Metodo (string usuario, string senha)
//     {
//         this.Usuario = usuario;
//         this.Senha = senha;
//     }
//     public bool Login (string usuario, string senha)
//     {
//         if (usuario == Usuario && senha == Senha)
//         {
//             return true;
//         }
//         else
//         {
//             throw new CredenciaisInvalidasException();
//         }
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         string usuario = "pessoa1";
//         string senha = "12345";

//         Metodo pessoa1 = new Metodo("pessoa1", "123456");

//         try
//         {
//             pessoa1.Login(usuario, senha);
//             Console.WriteLine("Sucesso!");
//         }
//         catch (CredenciaisInvalidasException ex)
//         {
//             Console.WriteLine($"Erro: {ex.Message}");
//         }

//         Metodo pessoa2 = new Metodo("pessoa2", "1234");

//         string usuario2 = "pessoa2";
//         string senha2 = "1234";

//         try
//         {
//             pessoa2.Login(usuario2, senha2);
//              Console.WriteLine("Sucesso!");
//         }
//         catch (CredenciaisInvalidasException ex)
//         {
//             Console.WriteLine($"Erro: {ex.Message}");
//         }
//     }
// }

// Exercício 4

// public class Metodo
// {
//     public static bool Idade(string idade)
//     {
//         int numero = int.Parse(idade);

//         if (numero < 0 && 120 < numero)
//         {
//             throw new ArgumentException("Idade inválida!");
//         }
//         else
//         {
//             Console.WriteLine($"Sua idade é: {numero}");
//             return true;
//         }
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         string idade = "asdasd";

//         try
//         {
//             Metodo.Idade(idade);
//         }
//         catch (FormatException ex)
//         {
//             Console.WriteLine($"Número inválido: {ex.Message}");
//         }
//         catch (ArgumentException ex)
//         {
//             Console.WriteLine($"Erro: {ex.Message}");
//         }
//     }
// }


// DEBUG

// static void ProcessarPedido(int quantidade, int estoqueDisponivel)
// {
//     try
//     {
//         int quantidadeRestante = quantidade / estoqueDisponivel; // bug proposital escondido aqui
//         Console.WriteLine($"Processado. Restam {quantidadeRestante} no estoque.");
//     }
//     catch (DivideByZeroException ex)
//     {
//         Console.WriteLine($"Erro: {ex.Message}");
//     }
// }

// ProcessarPedido(10, 0);

// Checkpoint


public class IdadeInvalidaException : ArgumentException
{
    public IdadeInvalidaException() : base("Idade inválida, tente novamente") { }
} 

public class FaltaDeDadosException : ArgumentException
{
    public FaltaDeDadosException() : base("Faltam dados, tente novamente") { }
}

public class NaoEncontradoException : ArgumentException
{
    public NaoEncontradoException() : base("Funcionário não encontrado!") { }
}

interface ICadastrar
{
    public void Cadastrar(Funcionario funcionario);
}
interface IRemover
{
    public void Remover(string name);
}
interface IProcurar
{
    public void Procurar(string name);
}
public abstract class Pessoa
{
    public static List<Pessoa> pessoas = new List<Pessoa>();
    private int Age;
    public string Name { get; set; }

    public int _Age
    {
        get { return Age; }
        set
        {
            if ( value < 0)
            {
                throw new IdadeInvalidaException();
            }

            Age = value;
        }
    }

    public Pessoa (string name, int age)
    {
        this.Name = name;
        this._Age = age;
    }


}

public class Funcionario : Pessoa, ICadastrar, IRemover, IProcurar
{
    public Funcionario(string name, int age) : base(name, age) { }

    public void Cadastrar(Funcionario funcionario)
    {
         if (pessoas.Contains(funcionario)) throw new FaltaDeDadosException();
        pessoas.Add(funcionario);
    }

    public void Remover(string name)
{
    Funcionario? encontrado = null;
    foreach (Funcionario funcionario in pessoas)
    {
        if (funcionario.Name == name)
        {
            encontrado = funcionario;
            break;
        }
    }

    if (encontrado == null)
        throw new NaoEncontradoException();

    pessoas.Remove(encontrado);
}

    public void Procurar(string name)
    {
        Funcionario? encontrado = null;
        foreach(Funcionario funcionario in pessoas)
        {
            if (funcionario.Name == name)
            {
                encontrado = funcionario;
                break;
            }
        }

        if (encontrado == null)
        {
             throw new NaoEncontradoException();
        }

        Console.WriteLine($"Pessoa encontrada: {encontrado}");
    }
}

public class Program
{
    public static void Main()
    {

        Funcionario funcionario = new Funcionario("Arthur", 18);

        funcionario.Cadastrar(funcionario);
        Console.WriteLine("Cadastrado com sucesso.");

        try
        {
            funcionario.Cadastrar(funcionario);
        }
        catch (FaltaDeDadosException ex)
        {
            Console.WriteLine($"Erro esperado: {ex.Message}");
        }

        funcionario.Procurar(funcionario.Name);

        // Testa sucesso do Remover
        funcionario.Remover(funcionario.Name);
        Console.WriteLine("Removido com sucesso.");

        try
        {
            funcionario.Procurar(funcionario.Name);
        }
        catch (NaoEncontradoException ex)
        {
            Console.WriteLine($"Erro esperado: {ex.Message}");
        }
    }

}
    
