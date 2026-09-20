using Microsoft.AspNetCore.Mvc;
using exercicios.Models;

namespace exercicios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    [HttpGet]
    public IActionResult getTodos()
    {
        var livros = new List<Livro>
        {
            new Livro { Id = 1, Titulo = "1984", Ano = 1949},
            new Livro { Id = 2, Titulo = "Dom Casmurro", Ano = 1899}
        };
        return Ok(livros);
    }
}