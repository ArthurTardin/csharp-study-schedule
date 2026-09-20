# Etapa 14 - Framework: ASP.NET Core

## 1. O que é uma API REST

**API** = Application Programming Interface - forma de dois programas falar em um com o outro.

**REST** = Representational State Transfer - padrão de design que usa HTTP direto:

- `GET /usuarios/1`: pega usuário com ID 1
- `POST /usuarios`: cria usuário novo
- `PUT /usuarios/1`: modifica usuário 1
- `DELETE /usuarios/1`: deleta usuário 1

Em vez de "chama função X com parâmetro Y", você faz **requisição HTTP** pra uma URL. Isso é universal, qualquer linguagem consegue fazer requisição HTTP.

---

## 2. ASP.NET Core - por que essa escolha

**Alternativas:**

- Node.js + Express (JavasScript, mais rápido de aprender, menos tipagem)
- Python + Flask/Django (rápido prototipagem, menos performance em escala)
- Go (performance, mas linguagem nova)
- Java + Spring (pesado, mas indústria usa muito)

**Por que ASP.NET Core aqui:**

- Você já sabe C#, não precisa aprender linguagem nova
- Tipagem forte, menos bugs em produção
- Performance, uma das mais rápidas
- Ecosistema coeso, EF Core, testes, injeção de dependência, tudo integrado
- Indústria, muito procurado, salário bom

**Trade-off:** Curva de aprendizado maior (framework é complexo), mas "magia" que Flask/Express

---

## 3. Criando seu primeiro projeto ASP.NET Core

```bash
dotnet new webapi -n MeuProjeto
cd MeuProjeto
dotnet run
```

Abre `http://localhost:5000` - você tem uma API rodando.

**Estrutura padrão:**

```
MeuProjeto/
├── Controllers/          # endpoints da API
├── Models/               # classes de domínio (Autor, Livro, etc)
├── Data/                 # DbContext, migrations
├── Program.cs            # configuração da aplicação
└── appsettings.json      # settings (banco, logs, etc)
```

---

## 4. Controllers - definir endpoints

Um **Controller** é uma classe que responde a requisições HTTP.

```csharp
using Microsoft.AspNetCore.Mvc;
 
[ApiController]
[Route("api/[controller]")]
public class AutoresController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<Autor> GetAutor(int id)
    {
        var autor = new Autor { Id = id, Nome = "Machado de Assis" };
        return Ok(autor); // retorna HTTP 200 + JSON
    }
    
    [HttpPost]
    public ActionResult<Autor> CreateAutor([FromBody] Autor autor)
    {
        // salva no banco
        return CreatedAtAction("GetAutor", new { id = autor.Id }, autor); // HTTP 201
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateAutor(int id, [FromBody] Autor autor)
    {
        // atualiza no banco
        return NoContent(); // HTTP 204
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteAutor(int id)
    {
        // deleta do banco
        return NoContent(); // HTTP 204
    }
}
```

**Detalhes:**

- `[Route("api/[controller]")]`: rota é `/api/autores` (controller name sem "Controller")
- `[HttpGet("{id}")]`: responde a `GET /api/autores/1`
- `[FromBody]`: dados vêm no corpo da requisição (JSON)
- `Ok()`: retorna HTTP 200
- `CreatedAtAction()`: retorna HTTP 201 + local do recurso criado
- `NoContent()`: retorna HTTP 204 (sucesso, sem corpo)

---

## 5. Dependency Injection (DI)

Você já conhece DI conceitualmente (injetar dependências via construtor). ASP.NET Core automatiza isso.

**EM `Program.cs`:**

```csharp
var builder = WebApplication.CreateBuilder(args);
 
// Registrar serviços
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ApplicationDbContext>();
 
var app = builder.Build();
 
app.Run();
```

**Em um Controller:**

```csharp
public class AutoresController : ControllerBase
{
    private readonly IAutorService service;
    
    public AutoresController(IAutorService service)
    {
        this.service = service; // injetado automaticamente
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var autores = await service.GetTodosAsync();
        return Ok(autores);
    }
}
```

ASP.NET Core vê que o controller precisa de `IautorService`, procura no registro(`AddScoped`), cria uma instância e passa. Você não cria `new` em lugar nenhum.

**Escopos:**

- `Singleton`: uma instância pra toda a aplicação
- `Scoped`: uma instância por requisição HTTP
- `Transient`: instância nova sempre
**Regra prática:** use `Scoped` pra serviços e `DbContext`, `Singleton` pra configuração.
 
---
 
## 6. Conectar banco de dados

**Em `Program.cs`:**
 
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db")
);
```
 
**Ou PostgreSQL:**
 
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=meu_banco")
);
```
 
Depois migrations funcionam normal:
 
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
 
---

## 7. Teste de integração - testar endpoint de verdade
 
Diferente de testes unitários, testes de integração testam o controller + banco + tudo junto.
 
```csharp
public class AutoresControllerTests
{
    [Fact]
    public async Task GetAutor_ComIdValido_RetornaAutor()
    {
        // Arrange - criar aplicação de teste
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        
        // Act - fazer requisição GET
        var response = await client.GetAsync("/api/autores/1");
        
        // Assert
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        
        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("Machado", json);
    }
}
```

`WebApplicationFactory` cria uma aplicação de teste completa, com banco em memória, pronta pra requisições.

---
 
## 8. Validação e tratamento de erro
 
**Validação com Data Annotations:**
 
```csharp
public class Autor
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, MinimumLength = 3)]
    public string Nome { get; set; } = "";
}
```
 
**No controller:**
 
```csharp
[HttpPost]
public async Task<IActionResult> CreateAutor([FromBody] Autor autor)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState); // HTTP 400 + erros de validação
    }
    
    // ... criar no banco
}
```
 
**Tratamento de exceção global:**
 
```csharp
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        
        var error = context.Features.Get<IExceptionHandlerPathFeature>();
        await context.Response.WriteAsync("Erro interno do servidor");
    });
});
```
 
---

## Checklist antes de ir pros exercícios
 
- [ ] Eu sei o que é uma API REST e por que usa HTTP?
- [ ] Eu sei criar um Controller com `[HttpGet]`, `[HttpPost]`, etc?
- [ ] Eu sei injetar dependências via construtor no controller?
- [ ] Eu sei conectar banco de dados no ASP.NET Core?
- [ ] Eu sei testar um endpoint com `WebApplicationFactory`?

---

## Exercícios
 
1. **CRUD API de livros**: criar controller com GET (todos + por ID), POST (criar), PUT (modificar), DELETE. Dados em memória (sem banco por enquanto, just mock data).
2. **Conectar banco de dados**: o mesmo CRUD acima, mas salvando/lendo de verdade no SQLite (ou PostgreSQL se resolver os problemas de autenticação)
3. **Injetar serviço**: criar interface `ILivroService` e classe `LivroService`, injetar no controller, usar o serviço em vez de lógica direto no controller
4. **Validação**: adicionar `[Required]`, `[StringLength]` nas classes, validar no controller, retornar HTTP 400 com erros se inválido

### [DEBUG]
Código abaixo com dois bugs: falta registrar `ILivroService` no DI (vai dar null reference), e falta `[FromBody]` num POST (parâmetro não é preenchido).
 
```csharp
// Em Program.cs - falta:
// builder.Services.AddScoped<ILivroService, LivroService>();
 
[HttpPost]
public IActionResult CreateLivro(Livro livro) // falta [FromBody]
{
    service.Create(livro);
    return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
}
```
 
Identifique ambos e corrija.

### [TESTE]
Teste de integração com `WebApplicationFactory` - testar que:

1. `GET /api/livros` retorna HTTP 200 com lista
2. `POST /api/livros` com dados válidos retorna HTTP 201
3. `POST /api/livros` com dados inválidos retorna HTTP 400