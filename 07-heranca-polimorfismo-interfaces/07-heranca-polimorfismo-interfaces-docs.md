# Etapa 7 - Herança, Polimorfismo e Interfaces

## 1. Herança

Uma classe pode **herdar** atributos e comportamentos de outra, reaproveitando código em vez de duplicar:

```CSharp
    class Animal
    {
        public string Nome { get; set; }

        public void Comer()
        {
            Console.WriteLine($"{Nome} está comendo.");
        }
    }

    class Cachorro : Animal // Cachorro herda de Animal
    {
        public void Latir()
        {
            Console.WriteLine($"{Nome} está latindo.");
        }
    }

    Cachorro rex = new Cachorro();
    rex.Nome = "Rex"; // veio de Animal
    rex.Comer(); // Veio de Animal
    rex.Latir(); // Próprio de Cachorro
```

`Animal` é a classe base (ou superclasse). `Cachorro` é a **classe derivada** (ou subclasse). `Cachorro` tem tudo que `Animal` tem, mais o que é próprio dele. A relação certa para usar herança é "é um", `Cachorro` **é um** `Animal`. Se a relação for "tem um" (ex: `Carro` tem um `Motor`), isso não é herança, é **composição** (assunto que vem implicitamente aqui, mas formalizado mais para frente).

### Construtor em herança e `base`

```CSharp
   class Animal
   {
    public string Nome { get; set; }

    public Animal (string nome)
    {
        Nome = nome;
    }
   } 

   class Cachorro : Animal
   {
    public string Raca { get; set; }
    public Cachorro(string nome, string raca) : base (nome) // chama o construtor de Animal
    {
        Raca = raca;
    }
   }
```

`: base(nome)` chama explicitamente o construtor de classe pai, passando o que ele precisa. Sem isso, se `Animal` não tiver construtor vazio disponível, o código não compila, a classe derivada é obrigada a alimentar o construtor da base de algum jeito.

---

## 2. virtual, override, sealed

Por padrão, um método da classe base **pode ser reaproveitado**, mas não redefinido de forma polimórfica, a menos que você sinalize isso explicitamente:

```Csharp
   class Animal
   {
    public virtual void FazerSom()
    {
        Console.WriteLine("Som genérico de animal. ");
    }
   } 

   class Cachorro : Animal
   {
    public override void FazerSom()
    {
        Console.WriteLine("woof!");
    }
   }

   class Gato : Animal
   {
    public override void FazerSom()
    {
        Console.WriteLine("Miau");
    }
   }
```

- `virtual` na classe base, sinaliza "esse método **pode** ser substituído por uma classe derivada".
- `override` na classe derivada, efetivamente substitui o comportamento.

**sem `virtual` na base, `override` não compila.** E sem `override` na derivada, mesmo que ela delcare um método com o mesmo nome, isso não é polimorfismo, é um método **novo**, escondendo o da base (isso se chama "method hiding", e é justamente o bug proposital do exercício de debug: código que parece polimórfico, mas não é, porque falta `override`).

### sealed

```Csharp
   public override sealed void FazerSom() { ... } 
```

Impede que uma classe **ainda mais derivada** (que herdasse de `Cachorro`, por exemplo) sobrescreva esse método de novo. Uso raro no nível atual, mas vale reconhecer se aparecer

---

## 3. Polimorfismo em ação

O poder real de `virtual`/`override` aparece quando você trata objetos diferentes de forma uniforme, através do tipo da classe base:

```Csharp
   List<Animal> animais = new List<Animal>
   {
    new Cachorro(),
    new Gato(),
   } 

   foreach (Animal animal in animais)
   {
        animal.FazerSom(); //chama a versão CORRETA de cada um, mesmo a lista sendo de "Animal"
   }
```

Isso imprime "Woof!" e depois "Miau!", mesmo a variável de loop sendo declarada como `Animal`, o C# sabe, em **runtime**, qual é o tipo real de cada objeto, e chama a versão certa. Isso é polimorfismo: **o mesmo código chama comportamentos diferentes**, dependendo do tipo real do objeto por trás da referência.

**Isso só funciona por causa do `virtual`/`override`**. Se `FazerSom` não fosse `virtual`, o loop chamaria a versão de `Animal` para **todos os itens**, independente do tipo real, imprimindo "Som genérico de animal." duas vezes, silenciosamente errado.

---

## 4. Classes abstratas

Uma classe que **não pode ser instanciada diretamente**, só serve como base para outras classes herdarem. Usada quando o conceito da classe base é genérico demais para existir por si só (não faz sentido criar um "Animal" genérico sem espécie):

```Csharp
   abstract class Animal
   {
        public string Nome { get; set; }

        public abstract void FazerSom(); // sem corpo, OBRIGA a classe derivada a implementar

        public void Dormir()
        {
            Console.WriteLine($"{Nome} está dormindo.");
        }
   } 

   class Cachorro : Animal
   {
        public override void FazerSom() // obrigatório, sem isso, não compila
        {
            Console.WriteLine("Woof!");
        }
   }
```

`new Animal()` agora é **erro de compilação**, você só pode instanciar `Cachorro`, `Gato`, etc. `abstract void FazerSom();` (sem corpo, terminando em `;`) força toda classe derivada a implementar esse método, é uma forma de garantir que ninguém "esqueça" de definir esse comportamento em uma nova espécie de animal.

---

## 5. Interfaces

Um contrato **puro**, define **o quê** uma classe deve fazer, sem dizer **como**. Diferente de classe abstrata, uma interface não tem estado (caompos) new implementação (na maioria dos casos):

```Csharp
   interface IMovable
   {
        void Mover();
   } 

   interface IRefuelable
   {
        void Abastecer();
   }

   class Carro : IMovable, IRefuelable // implementa DUAS interfaces, múltipla "herança" de contrato
   {
        public void Mover()
        {
            Console.WriteLine("Carro andando.");
        }

        public void Abastecer()
        {
            Console.WriteLine("Tanque abastecido.")
        }
   }
```

**Diferença chave com herança de classe:** uma classe só pode herdar de **uma classe base** (`class X : Y`, só um `Y`), mas pode implementar **várias** interfaces (`class X : IA, IB, IC`). Isso resolve um problema real: e se `Carro` precisasse "ser" tanto `IMovable` quanto `IRefuelable`, mas essas duas coisas não têm relação de herança natual entre si? Interface resolve isso sem forçar hierarquia artificial.

Convenção: nome de interface começa com `I` maiúsculo (`IMovable`, não `Movable`, é convenção da comunidade C#, não regra do compilador, mas universalmente seguida.)

**Quando usar classe abstrata vs interface**
- **Classe abstrata**: Quando existe comportamento **compartilhado real** entre as derivada (`Dormir()` no exemplo acima, implementado uma vez, herdado por todos).
- **Interface**: quando você só quer garantir que várias classes, possivelmente sem nenhuma relação entre si, implementem um certo conjunto de comportamentos, sem compartilhar código nenhum.

---

## Checklist antes de ir pros exercícios

- [X] Eu sei explicar a diferença entre "É um" (herança) e "tem um" (composição), com um exemplo próprio?
- [X] Eu sei por que um método sem `virtual` na base não pode ter `override` na derivada?
- [X] Eu sei descrever, com suas palavras, o que aconteceria no loop do item 3 se `FazerSom` não fosse `virtual`?
- [X] Eu sei quando escolher classe abstrata e quando escolher interface, com critério, não só "porque parece certo"?

---

## Exercícios

1. **Hierarquia de formas geométricas**: classe abstrata `Forma` com método abstrato `CalcularArea()`. Classes derivadas: `Circulo`, `Quadrado`, `Triangulo`, cada um implementando `CalcularArea()` corretamente.
2. **Sistema de veículos com interfaces**: interfaces `IMovable` (`Mover()`) e `IRefuelable` (`Abastecer()`). Classe `Carro` implementando as duas. Classe `Bicicleta` implementando só `IMvable` (bicicleta não abastece)
3. **Hierarquia de funcionários**: classe base `Funcionário` (`Nome`, `SalarioBase`, método virtual `CalcularSalario()`). Classes derivadas `Gerente` (Bônus fixo somado ao salário base) e `Vendedor`(Comissão sobre vendas somada ao salário base), cada um sobrescrevendo `CalcularSalario()` com a lógica própria
4. **Lista polimórfica**: Crie uma `List<Funcionario>` com pelo menos 2 `Gerente` e 2 `Vendedor`, percorra com foreach chamando `CalcularSalario()` de cada um, confirmando que cada tipo usa sua própria lógica

## [DEBUG]

Código abaixo com hierarquia de veículos onde o polimorfismo não funciona como esperado, um `override` foi esquecido. Ache e corrija, e explique (por escrito, não só corrigindo) por que o comportamento estava errado antes da correção.

```Csharp
    class Veiculo
{
    public string Nome { get; set; }

    public virtual string TipoDeMovimento()
    {
        return "Movimento genérico";
    }
}

class Aviao : Veiculo
{
    public string TipoDeMovimento() // bug aqui
    {
        return "Voando";
    }
}

class Barco : Veiculo
{
    public override string TipoDeMovimento()
    {
        return "Navegando";
    }
}

List<Veiculo> veiculos = new List<Veiculo> { new Aviao(), new Barco() };

foreach (Veiculo v in veiculos)
{
    Console.WriteLine(v.TipoDeMovimento());
}
```

## [TESTE]

Escreva testes xUnit para a hierarquia de `Funcionario` do exercício 3: Teste que `Gerente.CalcularSalario()` retorna o valor esperado, e que `Vendedor.CalcularSalario()` retorna o valor esperado, confirmando que cada um usa sua própria fórmula, não a da base.