using eAgenda.Core.Aplicacao.Compartilhado;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.Compartilhado;
using eAgenda.Core.Dominio.ModuloContato;
using FluentResults;
using eAgenda.Core.Dominio.ModuloAutenticacao;
using Microsoft.Extensions.Caching.Distributed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eAgenda.Core.Aplicacao.ModuloContato.Handlers;

public class ExcluirContatoCommandHandler(
    IRepositorioContato repositorioContato,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IDistributedCache cache,
    ILogger<ExcluirContatoCommandHandler> logger
) : IRequestHandler<ExcluirContatoCommand, Result<ExcluirContatoResult>>
{
    public async Task<Result<ExcluirContatoResult>> Handle(ExcluirContatoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await repositorioContato.ExcluirAsync(command.Id);

            await unitOfWork.CommitAsync();

            var cacheKey = $"contatos:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = new ExcluirContatoResult();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante a exclusão de {@Registro}.",
                command
            );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}