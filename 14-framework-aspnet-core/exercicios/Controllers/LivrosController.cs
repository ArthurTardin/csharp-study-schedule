using Microsoft.AspNetCore.Mvc;
using exercicios.Models;
using exercicios.Data;

namespace exercicios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public LivrosController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetTodos()
    {
        var livros = _context.Livros.ToList();
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public IActionResult GetLivro(int id)
    {
        var livro = _context.Livros.Find(id);
        if (livro == null) return NotFound();
        return Ok(livro);
    }

    [HttpPost]
    public IActionResult CreateLivro([FromBody] Livro livro)
    {
        _context.Livros.Add(livro);
        _context.SaveChanges();
        return CreatedAtAction("GetLivro", new { id = livro.Id}, livro);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLivro(int id, [FromBody] Livro livro)
    {
        var livro1 = _context.Livros.Find(id);
        if (livro1 == null) return NotFound();

        livro1.Titulo = livro.Titulo;
        livro1.Ano = livro.Ano;
        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLivro(int id)
    {
        var livro = _context.Livros.Find(id);
        if (livro == null) return NotFound();

        _context.Livros.Remove(livro);
        _context.SaveChanges();
        
        return NoContent();
    }
}