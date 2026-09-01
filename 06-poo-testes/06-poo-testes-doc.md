# Etapa 6 - Programação Orientada a Objetos (Fundamentos) + Introdução a Testes

## 1. Classes e objetos

**Classe** é um molde, define quais atributos e comportamentos um tipo de objeto vai ter, mas não é o objeto em si. **Objeto** (ou instância) é uma ocorrência real, criada a partir da classe, com valores próprios.

```CSharp
    class Pessoa
    {
        public string Nome;
        public int Idade;
    }

    Pessoa pessoa1 = new Pessoa();
    pessoa1.Nome = "Arthur";
    pessoa1.Idade = 17;

    Pessoa pessoa2 = new Pessoa();
    pessoa2.Nome = "Maria";
    pessoa2.Idade = 30;
```

`Pessoa` é a classem existe uma vez no código. `pessoa1` e `pessoa2` são objetos distintos, cada um com seus próprios valores de `Nome` e `Idade`, mesmo vindo do mesmo molde. Mudar `pessoa1.Nome` não afeta `pessoa2.Nome`, são espaços de memória diferentes.

---

## 2. Atributos e propriedades

O exemplo acima usa **campos públicos** (`public string Nome`), funciona, mas é considerado má prática em C# real, porque não dá controle nenhum sobre como o valor é lido ou alterado. O padrão correto é usar **propriedades**:

```CSharp
   class Pessoa
   {
    public string Nome { get; set; }
    public int Idade { get; set; }
   } 
```

Isso parece igual a um campo público na superfície, mas por baixo o compilador gera métodos oculto de leitura (`get`) e escrita (`set`). A vantagem aparece quando você precisa de lógica na hora de atribuir ou ler:

```CSharp
   class Pessoa
   {
    private int idade;

    public int Idade
    {
        get { return idade; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Idade não pode ser negativa. ");
                idade = value;
        }
    }
   } 
```

Aqui, `idade` (minúsculo) é o campo privado que guarda o valor real. `Idade` (maiúsculo) é a propriedade pública que controla acesso a esse campo. `value` é uma palavra-chave especial disponível dentro do `set`, representa o valor que está sendo atribuído. Tentar `pessoa.idade = -5` agora lança exceção em vez de aceitar um dado inválido silenciosamente.

---

## 3. Construtores

Método especial chamado automaticamente quando um objeto é criado com `new`. Usado para garantir que o objeto **nasça em uma estado válido**, em vez de ser criado vazio e preenchido depois campo por campo:

```CSharp
   class Pessoa
   {
        public string Nome { get; set; }
        public int Idade { get; set; }

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }
   } 

   Pessoa pessoa1 = new Pessoa("Arthur", 17); // obrigatório passar os dois valores agora
```

Sem construtor customizado, C# fornece um construtor padrão vazio implícito (`new Pessoa()`, sem parâmetros). No momento em que você escreve qualquer construtor customizado, o construtor vazio implícito desaparece, se quiser manter os dois, precisa declarar ambos explicitamente (isso conecta direto com sobrecarga, construtores podem ser sobrecarregados igual método comum).

---

## 4. This

Palavra-chave que se refere à instância atual do objeto, usado principalmente para desambiguar quando o nome do parâmetros é igual ao nome do campo/propriedade:

```CSharp
   class Pessoa
   {
        public string Nome { get; set; }
        public Pessoa (string nome)
        {
            this.Nome = nome; // this.Nome é a propriedade da classe, "nome" é o parâmetro recebido.
        }
   } 
```

Sem o `this.`, `Nome = nome;` ainda funcionaria nesse caso específico (C# resolveria pelo contexto), mas fica ambíguo para quem lê. `this` também é usado para um construtor chamar outro construtor da mesma classe (encadeamento), assunto que você vai ver se precisar de sobrecarga de construtor no exercício.

---

## 5. Encapsulamento

Princípio de esconder detalhes internos de implementação, expondo só o necessário. Os modificadores de acesso controlam isso:

`public` - Qualquer código, de qualquer lugar
`private` - só dentro da própria classe
`protected` - A própria classe e classes que herdam dela (assunto da Etapa 7)

Regra prática que vale adotar desde já: campos internos começam privados por padrão, e só ficam públicos através de propriedades controladas, se realmente precisar expor. Isso é o que evita que código externo altere o estado do objeto de forma inválida, sem passar pela validação do `set`.

---

## 6. Membros estáticos (static)

```CSharp
   class ContadorDePessoas
   {
        public static int Total = 0; // Pertence à CLASSE, compartilhado entre todas as instâncias
        public string Nome; // percence a cada OBJETO individualmente
        public ContadorDePessoas(string nome)
        {
            Nome = nome;
            Total++; // incrementa o contador compartilhado toda vez que um objeto é criado
        }
   } 
```

`Total` existe uma única vez, na classe, não uma cópia por objeto. Toda instância que oncrementa `Total` está mexendo no mesmo valor compartilhado. Isso é diferente de `Nome`, que cada objeto tem o seu próprio independente dos outros.

Método estático não pode acessar membros de instância diretamente, porque método estático roda "no nível da classe", sem um objeto específico associado, então não faz sentido ele tentar ler `this.Nome` sem saber de qual objeto.

---

## 7. Introdução a testes - xUnit

A partir de agora, todo exercício novo com lógica não-trivial ganha teste. Isso não é burocracia, é a forma de você confirmar que seu código faz o que você acha que faz, sem depender de rodar manualmente e olhar o output toda vez.

### Setup (referência rápida)

```CSharp
   dotnet new xunit -o NomeDoProjeto.Tests 
```

Isso cria um projeto de teste separado. Convenção comum: se seu projeto principal se chama `MeuProjeto`, o projeto de teste se chama `MeuProjeto.Tests`.

### Arrange, Act, Assert (AAA)

Estrutura padrão de todo teste unitário:

```CSharp
   using Xunit;

   public class ContaBancariaTests
   {
        [fact]
        public void Depositar_DeveAumentarSaldo()
        {
            //Arrange -- Prepara o cenário
            var conta = new ContaBancaria(100);
        }

        // Act -- Executa a ação que está sendo testada
        conta.Depositar(50);

        //Assert -- verifica se o resultado é o esperado
        Assert.Equal(150, conta.saldo);
   } 
```

- `[Fact]`: atributo do xUnit que marca um método como um teste executável (Sem isso, o xUnit não sabe que aquele método é um teste).
- **Arrange**: Monta o cenário necessário (cria objetos, define valores iniciais).
- **Act**: Executa a ação específica que você quer testar.
- **Assert**: verifica se o resultado bate com o esperado. Se não bater, o teste falha e te avisa exatamente o que esperava vs o que recebeu, isso substitui você ficar lendo `Console.WriteLine` manualmente tentando notar se algo está errado.

### Asserts comuns

```CSharp
   Assert.Equal(esperado, atual)/
   Assert.True(condicao);
   Assert.False(condicao);
   Assert.Throws<ArgumentException>(() => metodoQueDeveQuebrar()); 
```

---

## Checklist antes de ir pros exercícios

- [ ] Eu sei explicar a diferença entre classe e objeto com um exemplo próprio, não o do documento?
- [ ] Eu sei por que campo público direto é considerado pior prática que propriedade com `get/set`?
- [ ] eu sei por que um construtor customizado remove o construtor vazio implícito?
- [ ] Eu sei por que `Total` (static) é compartilhado entre instâncias, mas `Nome` (não-static) não é?
- [ ] Eu sei nomear as 3 partes do padrão Arrange-Act-Assert e o que cada uma faz?

---

## Exercícios

- **Classe Pessoa**: com propriedades `Nome` e `Idade` (validando que `Idade` não pode ser negativa via `set`), construtor obrigando os dois valores
- **Classe Produto**: com `Nome`, `Preco` (usando `decimal`, lembra da Etapa 2), `Quantidade`, e um método `ValorTotal()` que retorna `Preco * Quantidade`
- Sistema de cadastro simples: uma classe `Cadastro` que internamente usa uma `List<Pessoa>` (da Etapa 4), com métodos `Adicionar(Pessoa p)`, `Remover(string nome)`, `Buscar(string nome)`
- **Classe ContaBancaria**: com `Saldo` privado (não exposto diretamente), métodos `Depositar(decimal valor)` e `Sacar(decimal valor)`. `Sacar` deve impedir saldo negativo — decida você se isso lança exceção ou retorna `bool` de sucesso, e seja consistente
- **[DEBUG]**: código de `ContaBancaria` abaixo com bug de saldo negativo permitido. Ache e corrija.
```csharp
class ContaBancaria
{
    public decimal Saldo { get; set; }

    public ContaBancaria(decimal saldoInicial)
    {
        Saldo = saldoInicial;
    }

    public void Sacar(decimal valor)
    {
        Saldo = Saldo - valor;
    }
}
```
**[TESTE]**: escreva testes unitários (xUnit) para a classe `ContaBancaria` corrigida, cobrindo pelo menos: depósito aumenta saldo corretamente, saque válido diminui saldo corretamente, saque maior que saldo é rejeitado (não deixa saldo negativo).

---

## Checkpoint 2

Volte no exercício "Validador de CPF" da Etapa 5 e escreva testes unitários (xUnit) cobrindo pelo menos 3 casos de borda: CPF válido, CPF com letra, CPF com tamanho errado.