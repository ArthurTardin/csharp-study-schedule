using Microsoft.EntityFrameworkCore;
using exercicios.Data;
using exercicios.Models;

namespace exercicios.Services;

public class LivroService : ILivroService
{
    private readonly ApplicationDbContext _context;

    public LivroService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> ObterTodosAsync()
    {
        return await _context.Livros.ToListAsync();
    }

    public async Task<Livro?> ObterPorIdAsync(int id)
    {
        return await _context.Livros.FindAsync(id);
    }

    public async Task<Livro> CriarAsync(Livro livro)
    {
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();
        return livro;
    }

    public async Task<bool> AtualizarAsync(int id, Livro livro)
    {
        if (id != livro.Id) return false;

        var existe = await _context.Livros.AnyAsync(l => l.Id == id);
        if (!existe) return false;

        _context.Entry(livro).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var livro = await _context.Livros.FindAsync(id);
        if (livro == null) return false;

        _context.Livros.Remove(livro);
        await _context.SaveChangesAsync();
        return true;
    }
}