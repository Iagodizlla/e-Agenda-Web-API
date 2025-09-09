using eAgenda.WebApi.Models.ModuloContato;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using AutoMapper;
using MediatR;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("contatos")]
public class ContatoController(IMediator mediator, IMapper mapper) : ControllerBase
{
    

    [HttpPost]
    public async Task<ActionResult<CadastrarContatoResponse>> Cadastrar(CadastrarContatoRequest request)
    {
        var command = mapper.Map<CadastrarContatoCommand>(request);

        var result = await mediator.Send(command);

        if (result.IsFailed)
        {
            if (result.HasError(e => e.HasMetadata("TipoErro", m => m.Equals("RequisicaoInvalida"))))
            {
                var errosDeValidacao = result.Errors
                    .SelectMany(e => e.Reasons.OfType<IError>())
                    .Select(e => e.Message);

                return BadRequest(errosDeValidacao);
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var response = mapper.Map<CadastrarContatoResponse>(result.Value);

        return Created(string.Empty, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EditarContatoResponse>> Editar(Guid id, EditarContatoRequest request)
    {
        var command = mapper.Map<(Guid, EditarContatoRequest), EditarContatoCommand>((id, request));

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<EditarContatoResponse>(result.Value);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ExcluirContatoResponse>> Excluir(Guid id)
    {
        var command = mapper.Map<ExcluirContatoCommand>(id);

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarContatosResponse>> SelecionarRegistros(
        [FromQuery] SelecionarContatosRequest? request
    )
    {
        var query = mapper.Map<SelecionarContatosQuery>(request);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return BadRequest();

        var response = mapper.Map<SelecionarContatosResponse>(result.Value);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarContatoPorIdResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = mapper.Map<SelecionarContatoPorIdQuery>(id);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return NotFound(id);

        var response = mapper.Map<SelecionarContatoPorIdResponse>(result.Value);

        return Ok(response);
    }
}