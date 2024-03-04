using EpochFlow.Toolkit.Commands.Accounts;
using EpochFlow.Toolkit.Commands.Accounts.ApiKeys;
using EpochFlow.Toolkit.Commands.DataGenerator;
using EpochFlow.Toolkit.Commands.Sets;
using EpochFlow.Toolkit.Commands.Sets.Data;
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
                .WithDescription("List sets.");

            #endregion

            #region ApiKeys

            config.AddCommand<CreateApiKeyCommand>("create-key")
                .WithDescription("Creates a new api key.");
            config.AddCommand<GetApiKeyCommand>("get-key")
                .WithDescription("Gets a api key.");
            config.AddCommand<DeleteApiKeyCommand>("delete-key")
                .WithDescription("Deletes a api key.");
            config.AddCommand<ListApiKeysCommand>("list-keys")
                .WithDescription("List api keys.");
            config.AddCommand<EnableApiKeyCommand>("enable-key")
                .WithDescription("Enable api key.");
            config.AddCommand<DisableApiKeyCommand>("disable-key")
                .WithDescription("Disable api key.");
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

            config.AddCommand<GenerateDataCommand>("generate-data")
                .WithDescription("Posts a random measurement every 10 seconds for 5 minutes.");

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