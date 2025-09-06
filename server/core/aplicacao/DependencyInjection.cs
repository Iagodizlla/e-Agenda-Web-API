using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace eAgenda.Core.Aplicacao;

public static class DependencyInjection
{
    public static IServiceCollection AddCamadaAplicacao(
        this IServiceCollection services,
        ILoggingBuilder logging,
        IConfiguration configuration
    )
    {
        services.AddSerilogConfig(logging, configuration);

        var assembly = typeof(DependencyInjection).Assembly;

        var licenseKey = configuration["AUTOMAPPER_LICENSE_KEY"];

        if (string.IsNullOrWhiteSpace(licenseKey))
            throw new Exception("A variável AUTOMAPPER_LICENSE_KEY não foi fornecida.");

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);

            config.LicenseKey = licenseKey;
        });

        services.AddAutoMapper(config =>
        {
            config.LicenseKey = licenseKey;

        }, assembly);

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
   
    private static void AddSerilogConfig(this IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        var licenseKey = configuration["NEWRELIC_LICENSE_KEY"];

        if (string.IsNullOrWhiteSpace(licenseKey))
            throw new Exception("A variável NEWRELIC_LICENSE_KEY não foi fornecida.");

        var caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        var caminhoArquivoLogs = Path.Combine(caminhoAppData, "eAgenda", "erro.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(caminhoArquivoLogs, LogEventLevel.Error)
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "e-agenda-app",
                licenseKey: licenseKey
            )
            .CreateLogger();

        logging.ClearProviders();

        services.AddSerilog();
    }
}