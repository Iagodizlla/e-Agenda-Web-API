using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;

public class SelecionarContatoPorIdQueryHandler(
    IMapper mapper,
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

            var result = mapper.Map<SelecionarContatoPorIdResult>(registro);

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