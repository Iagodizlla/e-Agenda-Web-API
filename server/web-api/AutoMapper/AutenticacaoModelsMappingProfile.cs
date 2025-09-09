using AutoMapper;
using eAgenda.Core.Aplicacao.ModuloAutenticacao.Commands;
using eAgenda.WebApi.Models.ModuloAutenticacao;

namespace eAgenda.WebApi.AutoMapper;

public class AutenticacaoModelsMappingProfile : Profile
{
    public AutenticacaoModelsMappingProfile()
    {
        CreateMap<RegistrarUsuarioRequest, RegistrarUsuarioCommand>();
        CreateMap<AutenticarUsuarioRequest, AutenticarUsuarioCommand>();
    }
}