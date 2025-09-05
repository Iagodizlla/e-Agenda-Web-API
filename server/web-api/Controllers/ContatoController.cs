using eAgenda.WebApi.Models.ModuloContato;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ContatoController(IMediator mediator) : ControllerBase
{
    

    [HttpPost]
    public async Task<ActionResult<CadastrarContatoResponse>> Cadastrar(CadastrarContatoRequest request)
    {
        var command = new CadastrarContatoCommand(
             request.Nome,
             request.Telefone,
             request.Email,
             request.Empresa,
             request.Cargo
        );

        var result = await mediator.Send(command);

        if (result.IsFailed)
            return BadRequest();

        var response = new CadastrarContatoResponse(result.Value.Id);

        return Created(string.Empty, response);
    }

    [HttpGet]
    public async Task<ActionResult<SelecionarContatosResponse>> SelecionarRegistros(
        [FromQuery] SelecionarContatosRequest? request
    )
    {
        var query = new SelecionarContatosQuery(request?.Quantidade);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return BadRequest();

        var response = new SelecionarContatosResponse(
             result.Value.Contatos.Count,
             result.Value.Contatos
         );

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelecionarContatosResponse>> SelecionarRegistroPorId(Guid id)
    {
        var query = new SelecionarContatoPorIdQuery(id);

        var result = await mediator.Send(query);

        if (result.IsFailed)
            return NotFound(id);

        var response = new SelecionarContatoPorIdResponse(
            result.Value.Id,
            result.Value.Nome,
            result.Value.Telefone,
            result.Value.Email,
            result.Value.Empresa,
            result.Value.Cargo,
            result.Value.Compromissos
        );

        return Ok(response);
    }
}