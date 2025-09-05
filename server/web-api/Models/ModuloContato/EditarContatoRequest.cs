namespace eAgenda.WebApi.Models.ModuloContato;

public record EditarContatoRequest(
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);

public record EditarContatoResponse(
    string Nome,
    string Telefone,
    string Email,
    string? Empresa,
    string? Cargo
);