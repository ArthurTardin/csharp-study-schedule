using Microsoft.AspNetCore.Mvc;
using exercicios.Models;
using exercicios.Services;

namespace exercicios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Livro>>> ObterTodos()
    {
        var livros = await _livroService.ObterTodosAsync();
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Livro>> ObterPorId(int id)
    {
        var livro = await _livroService.ObterPorIdAsync(id);
        if (livro == null) return NotFound("Livro não encontrado.");

        return Ok(livro);
    }

    [HttpPost]
    public async Task<ActionResult<Livro>> Criar([FromBody] Livro livro)
    {
        var novoLivro = await _livroService.CriarAsync(livro);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoLivro.Id }, novoLivro);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Livro livro)
    {
        var atualizado = await _livroService.AtualizarAsync(id, livro);
        if (!atualizado) return BadRequest("Não foi possível atualizar o livro.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var deletado = await _livroService.DeletarAsync(id);
        if (!deletado) return NotFound("Livro não encontrado para exclusão.");

        return NoContent();
    }
}