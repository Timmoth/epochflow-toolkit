using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace EpochFlow.ApiClient
{
    public static class EpochFlowStartupExtensions
    {
        public static IServiceCollection AddEpochFlowV1(this IServiceCollection services, string apiKey,
            string accountId,
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
}