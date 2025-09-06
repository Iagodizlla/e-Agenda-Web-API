using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloContato;
using AutoMapper;
using eAgenda.Core.Aplicacao.Compartilhado;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;

public class SelecionarContatosQueryHandler(
    IMapper mapper,
    IRepositorioContato repositorioContato,
    ILogger<SelecionarContatosQueryHandler> logger
) : IRequestHandler<SelecionarContatosQuery, Result<SelecionarContatosResult>>
{
    public async Task<Result<SelecionarContatosResult>> Handle(SelecionarContatosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registros = query.Quantidade.HasValue ?
                await repositorioContato.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioContato.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarContatosResult>(registros);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro durante a seleção de {@Registros}.",
                query
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}