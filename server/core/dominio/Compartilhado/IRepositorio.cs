namespace eAgenda.Core.Dominio.Compartilhado;

public interface IRepositorio<T> where T : EntidadeBase<T>
{
    Task CadastrarAsync(T novoRegistro);
    Task<bool> EditarAsync(Guid idRegistro, T registroEditado);
    Task<bool> ExcluirAsync(Guid idRegistro);
    Task<List<T>> SelecionarRegistrosAsync();
    Task<List<T>> SelecionarRegistrosAsync(int quantidade);
    Task<T?> SelecionarRegistroPorIdAsync(Guid idRegistro);
}
