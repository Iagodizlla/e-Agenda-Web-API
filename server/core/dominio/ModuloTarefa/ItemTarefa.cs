using System.Diagnostics.CodeAnalysis;

namespace eAgenda.Core.Dominio.ModuloTarefa;

public class ItemTarefa
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Titulo { get; set; }
    public bool Concluido { get; set; }
    public Tarefa Tarefa { get; set; }

    [ExcludeFromCodeCoverage]
    public ItemTarefa() { }

    public ItemTarefa(string titulo, Tarefa tarefa) : this()
    {
        Id = Guid.NewGuid();
        Titulo = titulo;
        Tarefa = tarefa;
        Concluido = false;
        UsuarioId = tarefa.UsuarioId;
    }

    public void Concluir()
    {
        Concluido = true;
    }

    public void MarcarPendente()
    {
        Concluido = false;
    }
}