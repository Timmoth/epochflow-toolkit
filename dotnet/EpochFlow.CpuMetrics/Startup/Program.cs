using EpochFlow.CpuMetrics.CpuMetrics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace EpochFlow.CpuMetrics.Startup;

internal class Program
{
    private static int Main(string[] args)
    {
        var serviceCollection = ConfigureServices();

        var app = new CommandApp(new TypeRegistrar(serviceCollection));
        app.Configure(config =>
        {
            config.AddCommand<CpuMetricsCommand>("cpu-metrics")
                .WithDescription("Tracks CPU metrics.");
        });

        return app.Run(args);
    }

    private static ServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton(TimeProvider.System);
        services.AddHttpClient();

        services.AddLogging(configure =>
            configure
                .AddSimpleConsole(opts => { opts.TimestampFormat = "yyyy-MM-dd HH:mm:ss "; }));

        return services;
    }
}