using EpochFlow.Toolkit.Commands;
using EpochFlow.Toolkit.Commands.Sets;
using EpochFlow.Toolkit.Commands.Sets.Tags;
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
            #region Account

            config.AddCommand<GetAccountCommand>("get-account")
                .WithDescription("Gets your account.");

            #endregion

            #region Sets

            config.AddCommand<CreateSetCommand>("create-set")
                .WithDescription("Creates a new set.");
            config.AddCommand<GetSetCommand>("get-set")
                .WithDescription("Gets a set.");
            config.AddCommand<DeleteSetCommand>("delete-set")
                .WithDescription("Deletes a set.");
            config.AddCommand<ListSetsCommand>("list-sets")
                .WithDescription("List a sets.");

            #endregion

            #region Tags

            config.AddCommand<ListTagsCommand>("list-tags")
                .WithDescription("List all tags within a set.");
            
            config.AddCommand<DeleteTagCommand>("delete-tag")
                .WithDescription("Deletes a tag within a set.");
            #endregion

            #region Data

            config.AddCommand<GetDataCommand>("get-data")
                .WithDescription("Queries a set for data points.");

            #endregion
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