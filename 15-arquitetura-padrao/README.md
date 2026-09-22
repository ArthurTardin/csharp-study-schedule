# Etapa 15 - Arquitetura & Padrões (C#)

## Antes de começar

Você já está **aplicando** esses conceitos no Projeto Real (Etapa 16): Services, Interfaces, Injeção de Dependência, Separação em Camadas. Esta etapa é sobre **entender por quê** funcionam e **melhorar** o código que você já fez.

Não é teoria pura — é validação + refatoração do que existe.

---

## 1. SOLID Principles

**SOLID** = 5 princípios que fazem código escalável, testável, manutenível.

### 1.1 - S: Single Responsibility Principle (SRP)

**Regra:** Uma classe = uma responsabilidade. Se muda por mais de um motivo, viola SRP.

**Exemplo ERRADO:**
```csharp
public class UserService
{
    // Responsabilidade 1: Lógica de usuário
    public void CreateUser(string email, string senha) { }
    
    // Responsabilidade 2: Envio de email
    public void SendWelcomeEmail(string email) { }
    
    // Responsabilidade 3: Log
    public void LogUserCreation(string email) { }
}
```

Muda se: regra de usuário muda, provider de email muda, formato de log muda (3 motivos).

**Correto:**
```csharp
public class UserService
{
    private readonly IEmailService _emailService;
    private readonly ILogger _logger;
    
    public void CreateUser(string email, string senha)
    {
        // Apenas lógica de usuário
        var user = new User { Email = email, Senha = HashSenha(senha) };
        _emailService.EnviarBemvindo(email);
        _logger.Log($"User {email} criado");
    }
}

public interface IEmailService
{
    void EnviarBemvindo(string email);
}

public interface ILogger
{
    void Log(string mensagem);
}
```

Agora: cada classe tem **uma razão para mudar**.

### 1.2 — O: Open/Closed Principle (OCP)

**Regra:** Aberto pra extensão, fechado pra modificação. Adiciona feature sem alterar código existente.

**Exemplo ERRADO:**
```csharp
public class PagamentoProcessor
{
    public void Processar(string tipo, decimal valor)
    {
        if (tipo == "cartao")
            ProcessarCartao(valor);
        else if (tipo == "boleto")
            ProcessarBoleto(valor);
        else if (tipo == "pix")
            ProcessarPix(valor);
        // Cada novo pagamento = modificar essa classe
    }
}
```

**Correto (com Strategy Pattern):**
```csharp
public interface IPagamento
{
    void Processar(decimal valor);
}

public class CartaoPagamento : IPagamento
{
    public void Processar(decimal valor) { }
}

public class BBoletoPagamento : IPagamento
{
    public void Processar(decimal valor) { }
}

public class PixPagamento : IPagamento
{
    public void Processar(decimal valor) { }
}

public class PagamentoProcessor
{
    public void Processar(IPagamento pagamento, decimal valor)
    {
        pagamento.Processar(valor);
        // Novo tipo de pagamento? Cria classe que implementa IPagamento. Pronto.
    }
}
```

**Novo tipo = nova classe, sem modificar código existente.**

### 1.3 — L: Liskov Substitution Principle (LSP)

**Regra:** Subclasse pode substituir classe-base sem quebrar o código.

**Exemplo ERRADO:**
```csharp
public class Animal
{
    public virtual void Mover() { }
}

public class Passaro : Animal
{
    public override void Mover()
    {
        Voar();
    }
}

public class Pinguim : Passaro
{
    public override void Mover()
    {
        throw new NotImplementedException("Pinguim não voa");
    }
}

// Cliente
Animal animal = new Pinguim();
animal.Mover(); // Boom! Exception
```

**Correto:**
```csharp
public class Animal
{
    public virtual void Mover() { }
}

public class PassaroVoador : Animal
{
    public override void Mover() { Voar(); }
}

public class PassaroTerrestre : Animal
{
    public override void Mover() { Nadar(); }
}

// Cliente nunca quebra — qualquer Animal.Mover() funciona
```

### 1.4 — I: Interface Segregation Principle (ISP)

**Regra:** Interfaces pequenas, específicas. Não force classe a implementar método que não usa.

**Exemplo ERRADO:**
```csharp
public interface IWorker
{
    void Trabalhar();
    void Comer();
    void Dormir();
}

public class Robo : IWorker
{
    public void Trabalhar() { }
    public void Comer() { throw new NotImplementedException(); }
    public void Dormir() { throw new NotImplementedException(); }
}
```

**Correto:**
```csharp
public interface IWorker
{
    void Trabalhar();
}

public interface ILiving
{
    void Comer();
    void Dormir();
}

public class Robo : IWorker
{
    public void Trabalhar() { }
}

public class Pessoa : IWorker, ILiving
{
    public void Trabalhar() { }
    public void Comer() { }
    public void Dormir() { }
}
```

### 1.5 — D: Dependency Inversion Principle (DIP)

**Regra:** Dependa de abstrações (interfaces), não de implementações concretas.

**Exemplo ERRADO:**
```csharp
public class UserService
{
    private readonly SqlServerDatabase _db = new SqlServerDatabase();
    
    public void CreateUser(User user)
    {
        _db.Save(user); // Acoplado ao SQL Server
    }
}
```

Se mudar pro PostgreSQL, muda a classe.

**Correto:**
```csharp
public class UserService
{
    private readonly IDatabase _db;
    
    public UserService(IDatabase db)
    {
        _db = db;
    }
    
    public void CreateUser(User user)
    {
        _db.Save(user);
    }
}

public interface IDatabase
{
    void Save(User user);
}

// Injeção em Program.cs
builder.Services.AddScoped<IDatabase, SqlServerDatabase>();
// Trocar banco? Uma linha muda.
```

---

## 2. Clean Code

**Princípios de escrever código legível.**

### 2.1 — Nomes

**Ruim:**
```csharp
public class d { }
public int x = 5;
public void p() { }
public DateTime dt;
```

**Bom:**
```csharp
public class Usuario { }
public int QuantidadeTentativasLogin = 5;
public void ProcessarPagamento() { }
public DateTime DataCriacao;
```

**Regra:** Nome deve **dizer o que é**, sem ambiguidade. Se precisa de comentário, nome está errado.

### 2.2 — Funções pequenas

**Ruim:**
```csharp
public void ProcessarTarefa(Tarefa tarefa)
{
    // 50 linhas de validação
    if (...) { }
    if (...) { }
    
    // 50 linhas de lógica
    foreach (...) { }
    
    // 50 linhas de persistência
    _db.Save(tarefa);
}
```

**Bom:**
```csharp
public void ProcessarTarefa(Tarefa tarefa)
{
    ValidarTarefa(tarefa);
    ExecutarTarefa(tarefa);
    PersistirTarefa(tarefa);
}

private void ValidarTarefa(Tarefa tarefa) { }
private void ExecutarTarefa(Tarefa tarefa) { }
private void PersistirTarefa(Tarefa tarefa) { }
```

**Regra:** Método deve fazer **uma coisa** e fazer bem. Se tem 30+ linhas, quebra.

### 2.3 — Evitar "Magic Numbers" e "Magic Strings"

**Ruim:**
```csharp
if (usuario.Idade > 18 && usuario.Salario > 2500)
{
    // ...
}
```

Qual é o significado do 18 e 2500?

**Bom:**
```csharp
private const int IdadeMinimaPara Emprestimo = 18;
private const decimal SalarioMinimoParaEmprestimo = 2500m;

if (usuario.Idade > IdadeMinimaPara Emprestimo && usuario.Salario > SalarioMinimoParaEmprestimo)
{
    // ...
}
```

### 2.4 — Comentários (evite)

**Ruim:**
```csharp
// Incrementa x
x++;
```

Código já diz. Comentário é ruído.

**Bom:**
```csharp
tentativasLogin++;
```

Se precisa de comentário, **refatora o código** pra ficar óbvio.

**Exceção:** "por quê" (não "o quê"). Se decisão é não-óbvia:
```csharp
// Usamos batch insert aqui porque single inserts davam timeout com 10k+ registros
_db.BulkInsert(usuarios);
```

---

## 3. Design Patterns (mais usados em C#)

### 3.1 - Singleton

**Problema:** Precisa de apenas uma instância de um objeto.

```csharp
public class AppSettings
{
    private static AppSettings _instance;
    
    private AppSettings() { }
    
    public static AppSettings Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AppSettings();
            return _instance;
        }
    }
}

// Uso
AppSettings.Instance.ApiKey = "xyz";
```

**Em .NET moderno:** Use DI (injeção de dependência) com `.AddSingleton()`.

### 3.2 — Factory

**Problema:** Criar objetos sem conhecer a classe concreta.

```csharp
public interface IRepository { }
public class SqlRepository : IRepository { }
public class PostgresRepository : IRepository { }

public class RepositoryFactory
{
    public static IRepository Create(string tipo)
    {
        return tipo switch
        {
            "sql" => new SqlRepository(),
            "postgres" => new PostgresRepository(),
            _ => throw new ArgumentException()
        };
    }
}

// Uso
IRepository repo = RepositoryFactory.Create("postgres");
```

### 3.3 — Strategy

**Problema:** Múltiplas formas de fazer a mesma coisa. Escolhe em runtime.

```csharp
public interface IOrdenacao
{
    void Ordenar(List<int> lista);
}

public class OrdenacaoRapida : IOrdenacao
{
    public void Ordenar(List<int> lista) { /* QuickSort */ }
}

public class OrdenacaoSimples : IOrdenacao
{
    public void Ordenar(List<int> lista) { /* BubbleSort */ }
}

public class Algoritmo
{
    private readonly IOrdenacao _estrategia;
    
    public Algoritmo(IOrdenacao estrategia)
    {
        _estrategia = estrategia;
    }
    
    public void Executar(List<int> lista)
    {
        _estrategia.Ordenar(lista);
    }
}

// Uso
IOrdenacao estrategia = new OrdenacaoRapida();
var algo = new Algoritmo(estrategia);
algo.Executar(lista);
```

### 3.4 — Repository Pattern

**Problema:** Isolar lógica de acesso a dados.

```csharp
public interface ITarefaRepository
{
    void Adicionar(Tarefa tarefa);
    Tarefa ObterPorId(int id);
    List<Tarefa> ObterTodas();
    void Atualizar(Tarefa tarefa);
    void Deletar(int id);
}

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;
    
    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void Adicionar(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();
    }
    
    // Outros métodos...
}

// Em Program.cs
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();

// Em Controller
public class TarefasController
{
    private readonly ITarefaRepository _repo;
    
    public TarefasController(ITarefaRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public IActionResult GetTodas()
    {
        return Ok(_repo.ObterTodas());
    }
}
```

## Resumo

**Etapa 15 não é teoria pura, é:**
1. Entender **por que** as abstrações funcionam
2. Refatorar **seu próprio código** (Projeto Real)
3. Validar que aplicou SOLID + Clean Code
4. Commitar a refatoração no GitHub

**Prazo estimado:** 1-2 semanas, 3 horas/semana (em paralelo com Etapa 16).

**Próximo:** Etapa 17 (DevOps) ou continua refinando Etapa 16.

---

## Não há "terminado" nessa etapa

Arquitetura é **contínua.** Você vai refatorar código pro resto da sua carreira. Essa etapa é entender **por que** e **como** começar.