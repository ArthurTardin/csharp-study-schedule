using exercicios.Models;

namespace exercicios.Services;

public interface ILivroService
{
    Task<List<Livro>> ObterTodosAsync();
    Task<Livro?> ObterPorIdAsync(int id);
    Task<Livro> CriarAsync(Livro livro);
    Task<bool> AtualizarAsync(int id, Livro livro);
    Task<bool> DeletarAsync(int id);
}