// using System.Data;
// using Microsoft.Data.Sqlite;

// string connectionString = "Data Source=livros.db";

// using (SqliteConnection connection = new SqliteConnection(connectionString))
// {
//     connection.Open();

//     // Cria tabela
//     using (SqliteCommand command = new SqliteCommand(
//         "CREATE TABLE IF NOT EXISTS livros (id INTEGER PRIMARY KEY, titulo TEXT, autor TEXT, ano INTEGER)",
//         connection))
//     {
//         command.ExecuteNonQuery();
//     }

//     // Insere 3 livros
//     string[] livros = { "1984", "O Corvo", "Dom Casmurro" };
//     string[] autores = { "George Orwell", "Edgar Allan Poe", "Machado de Assis" };
//     int[] anos = { 1949, 1845, 1899 };

//     for (int i = 0; i < 3; i++)
//     {
//         using (SqliteCommand insertCmd = new SqliteCommand(
//             "INSERT INTO livros (titulo, autor, ano) VALUES (@titulo, @autor, @ano)",
//             connection))
//         {
//             insertCmd.Parameters.AddWithValue("@titulo", livros[i]);
//             insertCmd.Parameters.AddWithValue("@autor", autores[i]);
//             insertCmd.Parameters.AddWithValue("@ano", anos[i]);
//             insertCmd.ExecuteNonQuery();
//         }
//     }

//     // Lê todos
//     using (SqliteCommand selectCmd = new SqliteCommand("SELECT * FROM livros", connection))
//     {
//         using (SqliteDataReader reader = selectCmd.ExecuteReader())
//         {
//             while (reader.Read())
//             {
//                 Console.WriteLine($"Título: {reader["titulo"]}, Autor: {reader["autor"]}, Ano: {reader["ano"]}");
//             }
//         }
//     }
// }

// Exercício 2, 3 e 4

using Microsoft.EntityFrameworkCore;
namespace Project;

public class Autor
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public List<Livro> livros { get; set; } = new();
}

public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public int Ano { get; set; }
    public int AutorId { get; set; }
    public Autor? autor { get; set; }
}

public class ApplicationDbContext : DbContext
{
    public DbSet<Autor> autores { get; set; }
    public DbSet<Livro> livros { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public ApplicationDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
            options.UseSqlite("Data Source=biblioteca.db");
    }
}

public class Program
{
    public static void Main()
    {
       using (var context = new ApplicationDbContext())
        {
            var autor = new Autor {Nome = "George Orwell",};
            context.autores.Add(autor);
            context.SaveChanges();

            var livro1 = new Livro {Titulo = "1984", Ano = 1949, AutorId = autor.Id};
            var livro2 = new Livro {Titulo = "A revolução dos bichos", Ano = 1945, AutorId = autor.Id};

            context.livros.Add(livro1);
            context.livros.Add(livro2);
            context.SaveChanges();

            var autor1 = context.autores
                .Include(p => p.livros)
                .FirstOrDefault(a => a.Id == 1);

            Console.WriteLine("Autor: " + autor1!.Nome);

            foreach (var livro in autor1.livros)
            {
                Console.WriteLine("Livro: " + livro.Titulo);
            }
        }


    }
}

// DEBUG


// var autor = context.Autores
//     .Include(p => p.Livros)
//     .FirstOrDefault(a => a.Id == 1);
    

// Console.WriteLine($"Livros de {autor.Nome}:");
// foreach (var livro in autor.Livros) // Livros vazio porque faltou .Include()
// {
//     Console.WriteLine(livro.Titulo);
// }