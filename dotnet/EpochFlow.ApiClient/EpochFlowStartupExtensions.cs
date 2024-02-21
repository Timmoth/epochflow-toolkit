using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace EpochFlow.ApiClient;

public static class EpochFlowStartupExtensions
{
    public static T ConfigureSettingsAndGet<T>(this IServiceCollection services, IConfiguration configuration)
        where T : class
    {
        var settingsSection = configuration.GetRequiredSection(typeof(T).Name);
        services.Configure<T>(settingsSection);
        return settingsSection.Get<T>() ??
               throw new NullReferenceException(
                   $"Configuration section '{typeof(T).Name}' did not return type '{typeof(T).Name}'.");
    }

    public static IServiceCollection AddEpochFlowV1(this IServiceCollection services, string apiKey, string accountId,
        string apiUrl)
    {
        services.AddRefitClient<IEpochFlowV1>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(apiUrl.TrimEnd('/'));
                c.DefaultRequestHeaders.Add("X-Account-Id", accountId);
                c.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            })
            .AddPolicyHandler(Policy
                .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMinutes(5)));

        return services;
    }
}