using EpochFlow.Toolkit.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli;

namespace EpochFlow.Toolkit.Startup;

internal class Program
{
    private static int Main(string[] args)
    {
        var serviceCollection = ConfigureServices();

        var app = new CommandApp(new TypeRegistrar(serviceCollection));
        app.Configure(config =>
        {
            config.AddCommand<CreateSetCommand>("create-set")
                .WithDescription("Creates a new set.");
            config.AddCommand<DeleteSetCommand>("delete-set")
                .WithDescription("Deletes a set.");
            config.AddCommand<ListSetsCommand>("list-sets")
                .WithDescription("List a sets.");
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