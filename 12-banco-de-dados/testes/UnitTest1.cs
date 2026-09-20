using Microsoft.EntityFrameworkCore;
using Project;

namespace testes;

public class UnitTest1
{
   public static ApplicationDbContext CriarDbContextTestes()
{
    var caminhoDb = Path.Combine(Path.GetTempPath(), "teste.db");
    
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite($"Data Source={caminhoDb}")
        .Options;

    var context = new ApplicationDbContext(options);
    context.Database.EnsureCreated();
    return context;
}

    [Fact]
    public void TestName()
    {
        // Arrange
        var context = CriarDbContextTestes();

        var autor = new Autor { Nome = "Arthur" };
    
        // Act
        context.autores.Add(autor);
        context.SaveChanges();

        var autorLido = context.autores.FirstOrDefault(a => a.Id == autor.Id);
    
        // Assert
        Assert.NotNull(autorLido);
        Assert.Equal("Arthur", autorLido.Nome);
    }
}
