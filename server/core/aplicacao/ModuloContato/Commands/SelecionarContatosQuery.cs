using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;

public record SelecionarContatosQuery(int? Quantidade)
    : IRequest<Result<SelecionarContatosResult>>;

public record SelecionarContatosResult(ImmutableList<SelecionarContatosDto> Contatos);

public record SelecionarContatosDto(
    Guid Id,
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);