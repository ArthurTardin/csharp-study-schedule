using Microsoft.EntityFrameworkCore;
using exercicios.Models;

namespace exercicios.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Livro> Livros => Set<Livro>();
}