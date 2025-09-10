using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloAutenticacao.Commands;
using eAgenda.Core.Dominio.ModuloAutenticacao;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace eAgenda.Core.Aplicacao.ModuloAutenticacao.Handlers;
public class AutenticarUsuarioCommandHandler(
    SignInManager<Usuario> signInManager,
    UserManager<Usuario> userManager,
    ITokenProvider tokenProvider
) : IRequestHandler<AutenticarUsuarioCommand, Result<AccessToken>>
{
    public async Task<Result<AccessToken>> Handle(AutenticarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuarioEncontrado = await userManager.FindByEmailAsync(request.Email);

        if (usuarioEncontrado is null)
            return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro("Não foi possível encontrar o usuário requisitado."));

        var resultadoLogin = await signInManager.PasswordSignInAsync(
            usuarioEncontrado.UserName!,
            request.Senha,
            isPersistent: true,
            lockoutOnFailure: false
        );

        if (resultadoLogin.Succeeded)
        {
            var tokenAcesso = tokenProvider.GerarAccessToken(usuarioEncontrado);

            return Result.Ok(tokenAcesso);
        }

        if (resultadoLogin.IsLockedOut)
            return Result.Fail(ResultadosErro
                .RequisicaoInvalidaErro("Sua conta foi bloqueada temporariamente devido a muitas tentativas inválidas."));

        if (resultadoLogin.IsNotAllowed)
            return Result.Fail(ResultadosErro
                .RequisicaoInvalidaErro("Não é permitido efetuar login. Verifique se sua conta está confirmada."));

        if (resultadoLogin.RequiresTwoFactor)
            return Result.Fail(ResultadosErro
                .RequisicaoInvalidaErro("É necessário confirmar o login com autenticação de dois fatores."));

        return Result.Fail(ResultadosErro
            .RequisicaoInvalidaErro("Login ou senha incorretos."));
    }
}