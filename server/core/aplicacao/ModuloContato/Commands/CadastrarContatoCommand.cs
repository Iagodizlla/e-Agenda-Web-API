using FluentResults;
using MediatR;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;

public record CadastrarContatoCommand(
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
) : IRequest<Result<CadastrarContatoResult>>;

public record CadastrarContatoResult(Guid Id);