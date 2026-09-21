using Microsoft.EntityFrameworkCore;
using exercicios.Models;

namespace exercicios.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Livro> Livros { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}