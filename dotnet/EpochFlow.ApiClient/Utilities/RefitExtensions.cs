using Microsoft.Extensions.Logging;
using Refit;

namespace EpochFlow.ApiClient.Utilities
{
    public static class RefitExtensions
    {
        public static void LogIfError<T>(this ILogger logger, ApiResponse<T> apiResponse)
        {
            if (apiResponse.IsSuccessStatusCode) return;

            logger.LogError("[{method}] to '{uri}' failed with status code: '{status}'. {error}",
                apiResponse.RequestMessage?.Method, apiResponse.RequestMessage?.RequestUri, apiResponse.StatusCode,
                apiResponse.Error?.Content);
        }

        public static void LogIfError(this ILogger logger, HttpResponseMessage apiResponse)
        {
            if (apiResponse.IsSuccessStatusCode) return;

            logger.LogError("[{method}] to '{uri}' failed with status code: '{status}'.",
                apiResponse.RequestMessage?.Method,
                apiResponse.RequestMessage?.RequestUri, apiResponse.StatusCode);
        }
    }
}