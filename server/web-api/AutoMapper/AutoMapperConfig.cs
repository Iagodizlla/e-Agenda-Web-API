namespace eAgenda.WebApi.AutoMapper;

public static class AutoMapperConfig
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(config =>
        {
            var licenseKey = configuration["AUTOMAPPER_LICENSE_KEY"];

            if (string.IsNullOrWhiteSpace(licenseKey))
                throw new Exception("A variável AUTOMAPPER_LICENSE_KEY não foi fornecida.");

            config.LicenseKey = licenseKey;

        }, typeof(Program).Assembly);

        return services;
    }
}