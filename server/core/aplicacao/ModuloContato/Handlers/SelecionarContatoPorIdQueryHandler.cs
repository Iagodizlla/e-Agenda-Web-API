using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;

public class SelecionarContatoPorIdQueryHandler(
    IRepositorioContato repositorioContato,
    ILogger<SelecionarContatoPorIdQueryHandler> logger
) : IRequestHandler<SelecionarContatoPorIdQuery, Result<SelecionarContatoPorIdResult>>
{
    public async Task<Result<SelecionarContatoPorIdResult>> Handle(SelecionarContatoPorIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registro = await repositorioContato.SelecionarRegistroPorIdAsync(query.Id);

            if (registro is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(query.Id));

            var result = new SelecionarContatoPorIdResult(
                registro.Id,
                registro.Nome,
                registro.Telefone,
                registro.Email,
                registro.Empresa,
                registro.Cargo,
                registro.Compromissos.Select(r => new DetalhesCompromissoContatoDto(
                    r.Assunto,
                    r.Data,
                    r.HoraInicio,
                    r.HoraTermino
                )).ToImmutableList()
            );

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro durante a seleção de {@Registro}.",
                query
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}