using eAgenda.Core.Dominio.ModuloAutenticacao;
using FluentResults;
using MediatR;

namespace eAgenda.Core.Aplicacao.ModuloAutenticacao.Commands;

public record AutenticarUsuarioCommand(string Email, string Senha) : IRequest<Result<AccessToken>>;