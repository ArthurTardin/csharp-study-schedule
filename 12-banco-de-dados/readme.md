# Etapa 12 - Banco de Dados

## 1. ADO.NET - Acesso básico ao banco

ADO.NET é a forma "manual" de falar com o banco, você escreve a conexão, o comando SQL, executa, lê o resultado.

### Conexão e comando simples

```csharp
using System.Data;
using Npgsql; // driver PostgreSQL
 
string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=sua_senha;Database=seu_banco";
 
using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
{
    connection.Open();
    
    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM pessoas", connection))
    {
        using (NpgsqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int id = reader.GetInt32(0); // coluna 0
                string nome = reader.GetString(1); // coluna 1
                Console.WriteLine($"ID: {id}, Nome: {nome}");
            }
        }
    }
}
```

**O padrão `using`:** fehcar a conexão automaticamente, mesmo se ocorrer erro. Conexão aberta sem fechar é recurso desperdiçado, o banco só permite um número limitado de conexões simultâneas.

### Inserir dados (com proteção contra SQL injection)

```csharp
using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
{
    connection.Open();
    
    string nome = "João"; // pode vir de usuário
    
    // NUNCA faça: "INSERT INTO pessoas (nome) VALUES ('" + nome + "')"
    // Alguém coloca nome = "'; DROP TABLE pessoas; --" e seu banco se vai
    
    // SEMPRE use parâmetros:
    using (NpgsqlCommand command = new NpgsqlCommand(
        "INSERT INTO pessoas (nome) VALUES (@nome)", 
        connection))
    {
        command.Parameters.AddWithValue("@nome", nome);
        command.ExecuteNonQuery(); // executa, não retorna dados
    }
}
```

**`@nome` é um placeholder**, o banco recebe o valor separado do SQL, impossível injetar código.

### Problema do ADO.NET puro

- Verbose (muito código repetitivo)
- fácil errar fechamento de conexão/reader
- SQL como string é propenso a erro (typo no nome de coluno, SQL injection se esquecer de `@parametro`)

Por isso existe **Entity Framework Core**, abstração sobre ADO.NET que alimina esse boilerplate.


---

## 2. Entity Framework Core (EF Core) - ORM moderno

Um **ORM** (Object=Relational Mapping) mapeia tabelas do banco em classes C#. Você não escreve SQL direto, você trabalha com objetos, e o EF Core gera o SQL para você

### Instalação

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.PostgreSQL
```

### Defina suas classes (Code First)

```csharp
public class Pessoa
{
    public int Id { get; set; } // chave primária (detectado automaticamente por EF)
    public string Nome { get; set; } = "";
    public int Idade { get; set; }
}
 
public class Pedido
{
    public int Id { get; set; }
    public string Descricao { get; set; } = "";
    public decimal Valor { get; set; }
    public int PessoaId { get; set; } // chave estrangeira
    public Pessoa? Pessoa { get; set; } // navegação
}
```

### DbContext = configuração do banco

```csharp
using Microsoft.EntityFrameworkCore;
 
public class ApplicationDbContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
 
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=sua_senha;Database=seu_banco");
    }
}
```

**`DbSet<T>`** representa uma tabela. `Pessoas` é a tabela de pessoas, `Pedidos` é a tabela de pedidos.

### Migrations - sincronizar schema com código

Depois de definir as classes, você cria um "migration" (registro de mudança):

```bash
dotnet ef migrations add InitialCreate
```

Isso gera um arquivo em `Migrations/` com o SQL que cria as tabelas. Depois:

```bash
dotnet ef database update
```

Aplica as migrations e cria o banco de verdade. Se mudar uma classe (ex: adicionar campo), você cria nova migration:

```bash
dotnet ef migrations add AddFieldXyz
dotnet ef database update
```

---

## 3. Operações CRUD com EF Core

### Create (inserir)

```csharp
using (var context = new ApplicationDbContext())
{
    var pessoa = new Pessoa { Nome = "Arthur", Idade = 17 };
    context.Pessoas.Add(pessoa);
    context.SaveChanges(); // persiste no banco
}
```

### Read (ler)

```csharp
using (var context = new ApplicationDbContext())
{
    // todos os registros
    var pessoas = context.Pessoas.ToList();
    
    // com filtro (LINQ)
    var arthur = context.Pessoas
        .Where(p => p.Nome == "Arthur")
        .FirstOrDefault();
    
    // por ID (método específico)
    var pessoaId1 = context.Pessoas.Find(1);
}
```

### Update (modificar)

```csharp
using (var context = new ApplicationDbContext())
{
    var pessoa = context.Pessoas.Find(1);
    if (pessoa != null)
    {
        pessoa.Idade = 18;
        context.SaveChanges();
    }
}
```

### Delete (deletar)

```csharp
using (var context = new ApplicationDbContext())
{
    var pessoa = context.Pessoas.Find(1);
    if (pessoa != null)
    {
        context.Pessoas.Remove(pessoa);
        context.SaveChanges();
    }
}
```

---

## 4. Relacionamentos (conectando tabelas)

### Um-para-muitos (1:N)

Uma pessoa pode ter múltiplos pedidos. Você já viu isso acima:

```csharp
public class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public List<Pedido> Pedidos { get; set; } = new(); // colecção
}
 
public class Pedido
{
    public int Id { get; set; }
    public string Descricao { get; set; } = "";
    public int PessoaId { get; set; } // chave estrangeira
    public Pessoa? Pessoa { get; set; } // navegação
}
```

Quando você insere um pedido, você associa a uma pessoa:

```csharp
var pessoa = context.Pessoas.Find(1);
var pedido = new Pedido { Descricao = "Compra X", Valor = 100, Pessoa = pessoa };
context.Pedidos.Add(pedido);
context.SaveChanges();
```

Quando você lê uma pessoa com seus pedidos:

```csharp
var pessoa = context.Pessoas
    .Include(p => p.Pedidos) // carrega os pedidos associados
    .FirstOrDefault(p => p.Id == 1);
 
foreach (var pedido in pessoa.Pedidos)
{
    Console.WriteLine(pedido.Descricao);
}
```

`Include` é **essencial**, sem ele, `Pedidos` fica vazio. Isso se chama "lazy loading vs eager loading", você força carregar agora (`Include`, eager) em vez de deixar carregar depois, sob demanda (lazy).

### Muitos-para-muitos (N:N)

Um estudante pode ter múltiplas disciplinas, uma disciplina pode ter múltiplos estudantes. Você precisa de uma tabela de junção (junction table):

```csharp
public class Estudante
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public List<Disciplina> Disciplinas { get; set; } = new();
}
 
public class Disciplina
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public List<Estudante> Estudantes { get; set; } = new();
}
```

EF Core cria a tabela de junção automaticamente, você não vê ela, mas `Include` é obrigatório para carregar:

```csharp
var estudante = context.Estudantes
    .Include(e => e.Disciplinas)
    .FirstOrDefault(e => e.Id == 1);
```

---

## LINQ to Entities vs LINQ em memória

Lembra de LINQ (Etapa 9)? `LINQ to Entities` é LINQ que roda **no banco**, não em C#.

```csharp
// LINQ to Entities — roda NO BANCO
var resultado = context.Pessoas
    .Where(p => p.Idade > 18)
    .Select(p => p.Nome)
    .ToList(); // .ToList() força executar a query no banco
```

Sem `.ToList()`:

```csharp
// Sem .ToList() — é ainda uma query em aberto (IQueryable)
IQueryable<Pessoa> query = context.Pessoas.Where(p => p.Idade > 18);
// query não foi executada ainda — é só um "plano"
 
// Quando você faz foreach ou .ToList(), aí sim executa
foreach (var pessoa in query) // AGORA executa no banco
{
    Console.WriteLine(pessoa.Nome);
}
```

**Importante:** nem todo LINQ funciona em Entities. `String.Length` em C# pode virar `LEN()` no SQL, mas métodos customizados não conseguem, se você tentar usar um método que EF não sabe traduzir pro SQL, vai lançar exceção. Nesse caso, carrega para memória com `.ToList()` primeiro, depois faz LINQ:

```csharp
// Errado — EF não sabe traduzir MeuMetodo() pro SQL
var resultado = context.Pessoas
    .Where(p => p.MeuMetodo() == true)
    .ToList();
 
// Correto
var resultado = context.Pessoas
    .ToList() // carrega TUDO pra memória primeiro
    .Where(p => p.MeuMetodo() == true) // agora é LINQ em memória, não Entities
    .ToList();
```

Cuidado: carregar tudo com `.ToList()` em um grande banco é lento, você deveria filtrar no banco se possível.

---

## 6. Exceções comuns

| Exceção | Causa |
|---|---|
| `DbUpdateException` | Erro ao salvar — ex: chave duplicada, constraint violada |
| `InvalidOperationException` | Tentar usar contexto após `.Dispose()`, ou `DbContext` nulo |
| `NullReferenceException` | Acessar propriedade navegação sem `.Include()` |
| `Microsoft.Data.SqlClient.SqlException` | Erro SQL direto — ex: typo em nome de tabela (raro com Code First) |

---

## Checklist antes de ir pros exercícios
 
- [ ] Eu sei por quê SQL injection é perigoso e como `@parametro` protege?
- [ ] Eu sei a diferença entre ADO.NET (manual) e EF Core (automático)?
- [ ] Eu sei por quê `using` é essencial pra conexão/DbContext?
- [ ] Eu sei quando usar `.Include()` pra carregar relacionamentos?
- [ ] Eu sei que `.ToList()` em LINQ to Entities força execução no banco?

---

## Exercícios
 
1. **CRUD com ADO.NET puro**: crie uma tabela `livros` (id, titulo, autor, ano), insira 3 livros usando ADO.NET com parâmetros, depois leia todos e imprima
2. **DbContext e migrations**: defina classes `Autor` e `Livro` (1:N), crie migrations, aplique no PostgreSQL, e confirme que as tabelas foram criadas (use `\dt` no psql pra listar)
3. **Inserir dados com EF Core**: crie um autor e 2 livros associados a ele usando EF Core, confirma que ficou no banco
4. **Ler com relacionamento**: carregue um autor com seus livros usando `.Include()`, imprima nome do autor + titulo de cada livro

### [DEBUG]
Código abaixo com dois bugs: um `.Include()` faltando (lazy loading silencioso), e uma migration não aplicada (banco desincronizado com classes).
 
```csharp
var autor = context.Autores.FirstOrDefault(a => a.Id == 1);
Console.WriteLine($"Livros de {autor.Nome}:");
foreach (var livro in autor.Livros) // Livros vazio porque faltou .Include()
{
    Console.WriteLine(livro.Titulo);
}
```

Encontre ambos os bugs e corrija.

### [TESTE]
Testes unitários (com xUnit) pra operações CRUD. Use um banco de testes em memória (EF Core permite isso) ou fixture que limpa o banco entre testes. Teste: inserir um autor com livros, ler de volta, modificar, deletar. Sem mock de DbContext — use um DbContext real contra um banco fake/em memória.