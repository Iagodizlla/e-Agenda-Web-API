using FluentResults;
using MediatR;

namespace eAgenda.Core.Aplicacao.ModuloAutenticacao.Commands;

public record SairCommand : IRequest<Result>;