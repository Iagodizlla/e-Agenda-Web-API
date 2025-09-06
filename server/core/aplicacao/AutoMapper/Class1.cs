using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloContato.Commands;
using eAgenda.Core.Dominio.ModuloCompromisso;
using eAgenda.Core.Dominio.ModuloContato;
using System.Collections.Immutable;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eAgenda.Core.Aplicacao.AutoMapper;

public class ContatoMappingProfile : Profile
{
    public ContatoMappingProfile()
    {
        // Commands/Results de Cadastro
        CreateMap<CadastrarContatoCommand, Contato>();
        CreateMap<Contato, CadastrarContatoResult>();

        // Commands/Results de Edição
        CreateMap<EditarContatoCommand, Contato>();
        CreateMap<Contato, EditarContatoResult>();

        // Commands/Results de Seleção Por Id
        CreateMap<Contato, SelecionarContatoPorIdResult>()
            .ConvertUsing(src => new SelecionarContatoPorIdResult(
                src.Id,
                src.Nome,
                src.Telefone,
                src.Email,
                src.Empresa,
                src.Cargo,
                (src.Compromissos ?? Enumerable.Empty<Compromisso>())
                    .Select(r => new DetalhesCompromissoContatoDto(
                        r.Assunto,
                        r.Data,
                        r.HoraInicio,
                        r.HoraTermino
                    )).ToImmutableList()
            ));

        // Commands/Results de Seleção
        CreateMap<Contato, SelecionarContatosDto>();

        CreateMap<IEnumerable<Contato>, SelecionarContatosResult>()
            .ConvertUsing((src, dest, ctx) =>
                new SelecionarContatosResult(
                    src?.Select(c => ctx.Mapper.Map<SelecionarContatosDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarContatosDto>.Empty
                )
            );
    }
}