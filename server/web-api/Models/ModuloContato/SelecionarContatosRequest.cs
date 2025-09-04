using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using System.Collections.Immutable;

namespace eAgenda.WebApi.Models.ModuloContato;

public record SelecionarContatosRequest(int? Quantidade);

public record SelecionarContatosResponse(
    int Quantidade,
    ImmutableList<SelecionarContatosDto> Contatos
);