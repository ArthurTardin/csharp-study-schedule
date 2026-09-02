// Exercícios 1

// class Pessoa
// {
//     private int idade;
//     public string Nome { get; set; }

//     public int Idade
//     {
//         get { return idade; }
//         set
//         {
//             if (value < 0)  throw new ArgumentException("idade inválida.");
//             idade = value;
//         }
//     }
// }

// Exercício 2

// class Produto
// {
//     private decimal preco;
//     public string Nome { get; set; }
//     public int Quantidade { get; set; }

//    public Produto(string nome, int quantidade, decimal Preco)
//     {
//         this.Nome = nome;
//         this.Preco = Preco;
//         this.Quantidade = quantidade;
//     }

//     public decimal Preco
//     {
//         get { return preco; }
//         set
//         {
//             if (value <= 0) throw new ArgumentException("Preço inválido.");
//             preco = value;
//         }
//     }

//     public decimal ValorTotal()
//     {
//         return preco * Quantidade;
//     }
// }

// // Exercício 3

// class Pessoa
// {

//     private int idade;
//     public string Nome { get; set; }
    
//     public int Idade
//     {
//         get { return idade; }
//         set
//         {
//             if (value < 0)
//             {
//                 throw new ArgumentException("idade iválida.");
//             }
//             idade = value;
//         }
//     }
// }

// class Cadastrar
// {
//     static List<Pessoa> pessoas = new List<Pessoa>();

//     public bool Adicionar (Pessoa pessoa)
//     {
//         if (pessoas.Contains(pessoa)) return false;
//         pessoas.Add(pessoa);
//         return true;
//     }

//     public bool Remover(string nome)
//     {
//          foreach(Pessoa pessoa in pessoas)
//         {
//             if (pessoa.Nome == nome)
//             {
//                 pessoas.Remove(pessoa);
//                 return true;
//             }
//         }
//         return false;
//     }

//     public bool Procurar(string nome)
//     {
//          foreach(Pessoa pessoa in pessoas)
//         {
//             if (pessoa.Nome == nome)
//             {
//                 return true;
//             }
//         }
//         return false;
//     }
// }

// // Exercício 4

// class ContaBancaria
// {
//     private decimal saldo;

//     public ContaBancaria(decimal saldoInicial)
// {
//     Saldo = saldoInicial;
// }
//     public decimal Saldo
//     {
//         get { return saldo; }
//         set
//         {
//             if (value < 0)
//             {
//                 throw new ArgumentException("O valor não pode ser negativo.");
//             }
//             saldo = value;
//         }
//     }

//     public bool Depositar(decimal value)
//     {
//         if (value <= 0)
//         {
//             return false;
//         }
//         saldo += value;
//         return true;
//     }

//     public bool Sacar(decimal value)
//     {
//         if (value > saldo) return false;
//         if (value <= 0) return false;

//         saldo -= value;
//         return true;
//     }
// }

// debug

namespace project;
public class ContaBancaria
{
    private decimal saldo;

    public ContaBancaria(decimal saldoInicial)
    {
        Saldo = saldoInicial;
    }

    public decimal Saldo
    {
        get { return saldo; }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Valor inválido.");
            }

            saldo = value;
        }
    }

    public void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do depósito deve ser positivo.");

        Saldo += valor;
    }

    public void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ArgumentException("O valor do saque deve ser positivo.");

        if (valor > Saldo)
            throw new InvalidOperationException("Saldo insuficiente.");

        Saldo -= valor;
    }
}
