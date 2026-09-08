// Exercício 1

// public abstract class Forma
// {
//     public abstract double CalcularArea();

// }

// public class Circulo : Forma
// {
//     public double Raio { get; set; }

//     public Circulo (double raio)
//     {
//         this.Raio = raio;
//     }
//     public override double CalcularArea()
//     {
//         return Math.PI * (Raio * Raio);
//     }
// }

// public class Quadrado : Forma
// {
//     public double Lado { get; set; }

//     public Quadrado (double lado)
//     {
//         this.Lado = lado;
//     }

//     public override double CalcularArea()
//     {
//         return Lado * Lado;
//     }
// }

// public class Triangulo : Forma
// {
//     public double Base { get; set; }
//     public double Altura { get; set; }

//     public Triangulo (double base1, double altura)
//     {
//         this.Base = base1;
//         this.Altura = altura;
//     }

//     public override double CalcularArea()
//     {
//         return (Base * Altura) / 2;
//     }
// }

// public class Progrm
// {
//     public static void Main()
//     {
//         Circulo circulo = new Circulo(5);
//         Console.WriteLine(circulo.CalcularArea());

//         Quadrado quadrado = new Quadrado(5);
//         Console.WriteLine(quadrado.CalcularArea());

//         Triangulo triangulo = new Triangulo(8, 5);
//         Console.WriteLine(triangulo.CalcularArea());

//     }

// }

// Exercício 2

// interface IMovable
// {
//     public bool Mover();
// }

// interface IRefuelable
// {
//     public bool Abastecer();
// }

// public class Carro : IMovable, IRefuelable
// {
//      public bool Mover()
//     {
//         return true;
//     }

//     public bool Abastecer()
//     {
//         return true;  
//     }
// }

// public class Bicicleta : IMovable
// {
//     public bool Mover()
//     {
//         return true;
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         Carro carro = new Carro();
//         bool moveu = carro.Mover();
//         bool Abasteceu = carro.Abastecer();

//         Bicicleta bicicleta = new Bicicleta();
//         bool moveuBicicleta = bicicleta.Mover();

//         Console.WriteLine(moveu);
//         Console.WriteLine(Abasteceu);
//         Console.WriteLine(moveuBicicleta);
//     }
// }

// Exercício 3

namespace project;
public abstract class Funcionario
{
    public string nome { get; set; }
    public decimal SalarioBase { get; set; }

    public Funcionario (string nome, decimal salarioBase)
    {
        this.nome = nome;
        this.SalarioBase = salarioBase;
    }

    public virtual decimal CalcularSalario()
    {
        return SalarioBase;
    }
}

public class Gerente : Funcionario
{
    public decimal BonusFixo { get; set; }
    public Gerente (string nome, decimal salarioBase, decimal bonusFixo) : base(nome, salarioBase)
    {
        this.BonusFixo = bonusFixo;
    }

    public override decimal CalcularSalario()
    {
        return SalarioBase + BonusFixo;
    }
}

public class Vendedor : Funcionario
{
    public decimal TotalVendas { get; set; }
    public decimal PorcentualComissao { get; set; }
    public Vendedor (string nome, decimal salarioBase, decimal totalVendas, decimal porcentualComissao) : base (nome, salarioBase)
    {
        this.TotalVendas = totalVendas;
        this.PorcentualComissao = porcentualComissao;
    }

    public override decimal CalcularSalario()
    {
        return SalarioBase + (TotalVendas * (PorcentualComissao / 100));
    }
}

public class Program
{
    public static void Main()
    {
        // Gerente gerente = new Gerente("Carlos", 25000, 200);
        // Console.WriteLine( gerente.CalcularSalario());

        // Vendedor vendedor = new Vendedor("Pablo", 70000, 10000, 5);
        // Console.WriteLine(vendedor.CalcularSalario());

        // Exercício 4

        // List<Funcionario> funcionarios = new List<Funcionario>
        // {
        //   gerente,
        //   new Gerente("Pedro", 1500, 1000),
        //   vendedor,
        //   new Vendedor("Miguel", 3000, 5000, 10) 
        // };

        // foreach(Funcionario funcionario in funcionarios)
        // {
        //     Console.WriteLine(funcionario.CalcularSalario());
        // }
    }
}

// DEBUG

//  class Veiculo
// {
//     public string Nome { get; set; } = string.Empty;

//     public virtual string TipoDeMovimento()
//     {
//         return "Movimento genérico";
//     }
// }

// class Aviao : Veiculo
// {
//     public override string TipoDeMovimento() // bug aqui
//     {
//         return "Voando";
//     }
// }

// class Barco : Veiculo
// {
//     public override string TipoDeMovimento()
//     {
//         return "Navegando";
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         List<Veiculo> veiculos = new List<Veiculo> { new Aviao(), new Barco() };

//         foreach (Veiculo v in veiculos)
//         {
//             Console.WriteLine(v.TipoDeMovimento());
//         }
//     }
// }

