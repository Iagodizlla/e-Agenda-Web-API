using eAgenda.Core.Aplicacao.ModuloAutenticacao.Commands;
using eAgenda.Core.Dominio.ModuloAutenticacao;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eAgenda.Core.Aplicacao.ModuloAutenticacao.Handlers;

public class SairCommandHandler(
    SignInManager<Usuario> signInManager
) : IRequestHandler<SairCommand, Result>
{
    public async Task<Result> Handle(SairCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return Result.Ok();
    }
}