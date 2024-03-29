﻿using EpochFlow.ApiClient.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace EpochFlow.ApiClient;

public static class EpochFlowStartupExtensions
{
    public static IServiceCollection AddEpochFlowV1(this IServiceCollection services, string apiKey,
        string apiUrl)
    {
        services.AddRefitClient<IEpochFlowV1>(new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(SerializerExtensions.Options)
            })
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(apiUrl.TrimEnd('/'));
                c.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            })
            .AddPolicyHandler(Policy
                .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMinutes(5)));

        return services;
    }
}