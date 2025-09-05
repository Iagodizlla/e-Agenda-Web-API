using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using System.Collections.Immutable;

namespace eAgenda.WebApi.Models.ModuloContato;

public record SelecionarContatoPorIdRequest(Guid Id);

public record SelecionarContatoPorIdResponse(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo,
    ImmutableList<DetalhesCompromissoContatoDto> Compromissos
);