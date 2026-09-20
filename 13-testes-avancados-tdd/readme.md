# Etapa 13 - Testes Avançados e TDD

## 1. TDD - Red-Green-Refactor

**TDD é um ciclo:**

1. **Red:** Você escreve um teste que **falha** (porque o código que testa ainda não existe)
2. **Green:** Você escreve o código **mínimo** para fazer o teste passar
3. **Refactor:** Você melhora o código, sem quebrar o teste

Exemplo:

```csharp
// RED - teste falha porque função não existe
[Fact]
public void CalcularDesconto_Com10Porcento_RetornaValorCorreto()
{
    var valor = 100;
    var desconto = 10;
    
    var resultado = CalcularDesconto(valor, desconto);
    
    Assert.Equal(90, resultado);
}
 
// GREEN - código mínimo pra passar
public int CalcularDesconto(int valor, int desconto)
{
    return valor - (valor * desconto / 100);
}
 
// REFACTOR - melhorar sem quebrar o teste
public decimal CalcularDesconto(decimal valor, int desconto)
{
    if (desconto < 0 || desconto > 100)
        throw new ArgumentException("Desconto deve estar entre 0 e 100");
    
    return valor * (1 - desconto / 100m);
}
```

Cada refactor mantém os testes passando, garantindo que você não quebrou nada.

---

## 2. Testes unitários vs Testes de Integração

**Unitário:** Testa uma **coisa só**, isolada de dependências:

```csharp
[Fact]
public void ValidarEmail_ComEmailValido_RetornaTrue()
{
    var email = "arthur@example.com";
    
    var resultado = ValidarEmail(email);
    
    Assert.True(resultado);
}
 
public bool ValidarEmail(string email)
{
    return email.Contains("@");
}
```

Rápido, determinístico, não depende de nada externo.

**Integração:** testa múltiplas partes **juntas**, com dependência reais:

```csharp
[Fact]
public void CriarPedido_ComDadosValidos_SalvaNoDbEEnviaEmail()
{
    var db = new ApplicationDbContext(); // banco real
    var emailService = new EmailService(); // serviço real
    
    var pedido = new Pedido { Cliente = "Arthur", Valor = 100 };
    
    var resultado = CriarPedido(pedido, db, emailService);
    
    Assert.NotNull(db.Pedidos.Find(resultado.Id)); // verificar no banco
    // verificar que email foi enviado
}
```

Mais lento, exigente (precisa banco ligado, serviço de email, etc), mas testa cenários reais.

**Melhor prática:** maioria unitário (fast feedback), alguns de integração (validar comportamento real).

---

## 3. Mocking com Moq

**Mock** é uma "falsificação" de uma dependência, você simula o comportamento sem usar a coisa real.

Exemplo: você quer testar uma classe que **depende** de um serviço de email. Em vez de usar o serviço real, você cria um mock.

### Instalação

```bash
dotnet add package Moq
```

### Exemplo prático

Código que você quer testar:

```csharp
public interface IEmailService
{
    void EnviarEmail(string destinatario, string mensagem);
}
 
public class CriarPedidoService
{
    private readonly IEmailService emailService;
    
    public CriarPedidoService(IEmailService emailService)
    {
        this.emailService = emailService;
    }
    
    public void CriarPedido(string cliente, decimal valor)
    {
        // simula criar pedido
        Console.WriteLine($"Pedido criado para {cliente}");
        
        // notifica o cliente
        emailService.EnviarEmail(cliente, "Pedido confirmado");
    }
}
```

**Teste sem mock (integração):**

```csharp
[Fact]
public void CriarPedido_DevemEnviarEmail()
{
    var emailService = new EmailService(); // real, lento
    var service = new CriarPedidoService(emailService);
    
    service.CriarPedido("arthur@example.com", 100);
    
    // como validar que email foi enviado? Difícil sem mock.
}
```

**Teste com mock (unitário):**

```csharp
[Fact]
public void CriarPedido_DevemEnviarEmail()
{
    var mockEmail = new Mock<IEmailService>();
    var service = new CriarPedidoService(mockEmail.Object);
    
    service.CriarPedido("arthur@example.com", 100);
    
    // verifica que EnviarEmail foi chamado com os parâmetros certos
    mockEmail.Verify(m => m.EnviarEmail("arthur@example.com", "Pedido confirmado"), Times.Once());
}
```

O mock **registra** todas as chamadas. Você valida com `.Verify()` que a chamada foi feita como esperado.

### Setup de comportamento do mock

```csharp
var mockPagamento = new Mock<IServiçoPagamento>();
 
// Mock retorna true quando perguntado
mockPagamento
    .Setup(m => m.Processar(It.IsAny<decimal>()))
    .Returns(true);
 
// Mock lança exceção quando perguntado
mockPagamento
    .Setup(m => m.Reembolsar(It.IsAny<decimal>()))
    .Throws(new Exception("Reembolso falhou"));
 
// Mock retorna diferentes valores em chamadas sucessivas
mockPagamento
    .SetupSequence(m => m.VerificarSaldo())
    .Returns(100m)
    .Returns(50m)
    .Returns(0m);
```

`It.IsAny<decimal>()` significa "qualquer valor decimal", útil quando você não quer fixar parâmetros específicos.

---

## 4. Fixtures - setup compartilhado

Se múltiplos testes precisam do **mesmo setup**, use uma fixture:

```csharp
public class CalculadoraFixture : IDisposable
{
    public Calculadora Calculadora { get; set; }
    
    public CalculadoraFixture()
    {
        Calculadora = new Calculadora();
    }
    
    public void Dispose()
    {
        // cleanup, se necessário
    }
}
 
public class CalculadoraTestes : IClassFixture<CalculadoraFixture>
{
    private readonly CalculadoraFixture fixture;
    
    public CalculadoraTestes(CalculadoraFixture fixture)
    {
        this.fixture = fixture;
    }
    
    [Fact]
    public void Somar_Com2E3_Retorna5()
    {
        var resultado = fixture.Calculadora.Somar(2, 3);
        Assert.Equal(5, resultado);
    }
    
    [Fact]
    public void Subtrair_Com5E3_Retorna2()
    {
        var resultado = fixture.Calculadora.Subtrair(5, 3);
        Assert.Equal(2, resultado);
    }
}
```

A fixture é criada uma vez e compartilhada entre testes da classe. Reduz duplicação de setup.

---

## 5. Testes parametrizados

Testar a mesma lógica com múltiplos valores:

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(0, 0, 0)]
[InlineData(-1, 1, 0)]
[InlineData(10, -5, 5)]
public void Somar_ComValoresVariados_RetornaCorreto(int a, int b, int esperado)
{
    var resultado = Somar(a, b);
    Assert.Equal(esperado, resultado);
}
```

`[Theory]` em vez de `[Fact]`, e `[InLineData]` para cada caso. Reduz muito código repetitivo, um método testa 4 cenários em vez de 4 métodos.

---

## 6. Validar exceções

```csharp
[Fact]
public void Dividir_PorZero_LancaExcecao()
{
    // Assert vem **antes** do Act
    var ex = Assert.Throws<DivideByZeroException>(() => Dividir(10, 0));
    
    // Opcional: validar mensagem
    Assert.Contains("divisão por zero", ex.Message);
}
 
[Fact]
public async Task BaixarDados_URLInvalida_LancaExcecao()
{
    var ex = await Assert.ThrowsAsync<HttpRequestException>(
        () => BaixarDados("https://invalid.invalid")
    );
}
```

---

## 7. Cobertura de testes

**Cobertura** = quanto do seu código é executado por testes

```bash
dotnet add package coverlet.collector
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

Gera relatório mostrando quantas linhas/branches foram testadas. Não é um número absoluto (100% de cobertura não garante código bom), mas ajuda a identificar código sem teste nenhum.

---

## Checklist antes de ir pros exercícios
 
- [ ] Eu sei o ciclo Red-Green-Refactor de TDD?
- [ ] Eu sei a diferença entre teste unitário (isolado) e integração (com dependências reais)?
- [ ] Eu sei como criar um mock com Moq e validar com `.Verify()`?
- [ ] Eu sei usar `[Theory]` + `[InlineData]` pra testar múltiplos casos?
- [ ] Eu sei validar que uma exceção é lançada com `Assert.Throws`?

---

## Exercícios
 
1. **TDD de calculadora**: escrever testes ANTES do código. Teste soma, subtração, multiplicação, divisão (com divisão por zero lançando exceção). Código mínimo pra passar cada teste.
2. **Mock de serviço externo**: classe `PedidoService` que depende de `IEmailService`. Teste que criar um pedido válido chama `EnviarEmail` uma vez. Teste que criar com dados inválidos não chama email.
3. **Testes parametrizados**: validar um email com `[Theory]` + `[InlineData]`, teste 5 emails válidos e 5 inválidos numa mesma função.
4. **Fixture compartilhada**: dois testes que compartilham setup de um repositório fake. Use `IClassFixture` pra reduzir duplicação.

### [DEBUG]
Código abaixo com dois bugs: mock que não foi configurado corretamente (retorna null), e `.Verify()` com `Times.Never()` quando deveria ser `Times.Once()`.
 
```csharp
var mock = new Mock<IEmailService>();
// Não configurou o mock, mock.SendEmail vai retornar void, que é ok, mas...
 
var service = new PedidoService(mock.Object);
service.CreateOrder("Arthur", 100);
 
// Bug: deveria ser Times.Once(), não Times.Never()
mock.Verify(m => m.SendEmail(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
```

Identifique ambos e corrija

### [TESTE]
Testes para uma classe `ValidadorSenha` que valida senhas:
- Mínimo 8 caracteres
- Pelo menos 1 letra maiúscula
- Pelo menos 1 número
- Retorna `true` se válida, `false` se não
Use `[Theory]` + `[InlineData]` pra testar 10 casos (5 válidas, 5 inválidas).