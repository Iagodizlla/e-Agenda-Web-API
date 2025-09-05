using FluentResults;
using MediatR;

namespace eAgenda.Core.Aplicacao.ModuloContato.Commands;

public record ExcluirContatoCommand(Guid Id) : IRequest<Result<ExcluirContatoResult>>;

public record ExcluirContatoResult();