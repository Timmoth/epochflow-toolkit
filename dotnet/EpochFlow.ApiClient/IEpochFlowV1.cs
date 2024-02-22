using EpochFlow.ApiClient.Accounts;
using EpochFlow.ApiClient.Analytics;
using EpochFlow.ApiClient.Data;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Permissions;
using EpochFlow.ApiClient.Sets;
using EpochFlow.ApiClient.Webhooks;
using Refit;

namespace EpochFlow.ApiClient
{
    public interface IEpochFlowV1
    {
        #region Webhooks

        [Delete("/api/v1/sets/{id}/webhooks/timeseries_updated")]
        public Task<HttpResponseMessage> DeleteTimeSeriesUpdatedWebhook(string id);

        [Get("/api/v1/sets/{id}/webhooks/timeseries_updated")]
        public Task<ApiResponse<TimeSeriesUpdatedWebhookConfig>> GetTimeSeriesUpdatedWebhook(string id);

        [Post("/api/v1/sets/{id}/webhooks/timeseries_updated")]
        public Task<ApiResponse<TimeSeriesUpdatedWebhookConfig>> CreateTimeSeriesUpdatedWebhook(string id,
            [Body] CreateTimeSeriesUpdatedWebhook request);

        [Patch("/api/v1/sets/{id}/webhooks/timeseries_updated")]
        public Task<ApiResponse<TimeSeriesUpdatedWebhookConfig>> UpdateTimeSeriesUpdatedWebhook(string id,
            [Body] UpdateTimeSeriesUpdatedWebhook request);

        #endregion

        #region Tags

        [Delete("/api/v1/sets/{id}/tags")]
        public Task<HttpResponseMessage> DeleteTag(string id, [Query] [AliasAs("tag")] string tag);

        [Get("/api/v1/sets/{id}/tags")]
        public Task<ApiResponse<List<string>>> ListTags(string id);

        #endregion

        #region Account

        [Patch("/api/v1/accounts")]
        public Task<ApiResponse<Account>> UpdateAccount(UpdateAccount request);

        [Get("/api/v1/accounts")]
        public Task<ApiResponse<Account>> GetAccount();

        [Delete("/api/v1/accounts")]
        public Task<HttpResponseMessage> DeleteAccount();

        #endregion

        #region Sets

        [Get("/api/v1/sets/{id}")]
        public Task<ApiResponse<Set>> GetSet(string id);

        [Delete("/api/v1/sets/{id}")]
        public Task<HttpResponseMessage> DeleteSet(string id);

        [Post("/api/v1/sets")]
        public Task<ApiResponse<Set>> CreateSet([Body] CreateSet request);

        [Patch("/api/v1/sets/{id}")]
        public Task<ApiResponse<Set>> UpdateSet(string id, [Body] UpdateSet request);

        [Get("/api/v1/sets")]
        public Task<ApiResponse<List<Set>>> ListSets();

        #endregion

        #region Api keys

        [Get("/api/v1/accounts/keys/{keyName}")]
        public Task<ApiResponse<ApiKey>> GetApiKey(string keyName);

        [Delete("/api/v1/accounts/keys/{keyName}")]
        public Task<HttpResponseMessage> DeleteApiKey(string keyName);

        [Post("/api/v1/accounts/keys")]
        public Task<ApiResponse<string>> CreateApiKey([Body] CreateApiKey request);

        [Get("/api/v1/accounts/keys")]
        public Task<ApiResponse<List<ApiKey>>> ListApiKeys();

        [Post("/api/v1/accounts/keys/{keyName}/enable")]
        public Task<HttpResponseMessage> EnableApiKey(string keyName);

        [Post("/api/v1/accounts/keys/{keyName}/disable")]
        public Task<HttpResponseMessage> DisableApiKey(string keyName);

        #endregion

        #region Data

        [Get("/api/v1/sets/{setId}/export")]
        Task<HttpResponseMessage> Export(string setId, CancellationToken cancellationToken = default);

        [Multipart]
        [Post("/api/v1/sets/{setId}/import")]
        Task<HttpResponseMessage> Import(string setId,
            [AliasAs("zipFile")] StreamPart zipFileContent,
            CancellationToken cancellationToken = default);

        [Get("/api/v1/sets/{id}/data")]
        public Task<ApiResponse<List<ResponseDataPoint>>> GetData(string id,
            [Query] GetDataRequest request);

        [Get("/api/v1/sets/{id}/data/analytics")]
        public Task<ApiResponse<AnalyticsData>> GetAnalytics(string id,
            [Query] GetAnalytics request);

        [Get("/api/v1/sets/{id}/data/anomalies")]
        public Task<ApiResponse<List<DataPoint>>> GetAnomalies(string id,
            [Query] [AliasAs("start")] long? startParam,
            [Query] [AliasAs("end")] long? endParam,
            [Query] [AliasAs("tag")] string tag,
            [Query] [AliasAs("resolution")] QueryResolution? resolution
        );

        [Get("/api/v1/sets/{id}/data/seasonality")]
        public Task<ApiResponse<SeasonalityResponse>> GetSeasonality(string id,
            [Query] [AliasAs("tag")] string tag
        );

        [Get("/api/v1/sets/{id}/data/latest")]
        public Task<ApiResponse<LatestDataPoints>> GetLatest(string id,
            [Query] GetLatestRequest request);

        [Get("/api/v1/sets/{id}/data/total")]
        public Task<ApiResponse<List<TagTotals>>> GetTotal(string id, [Query] GetTotalRequest request);

        [Post("/api/v1/sets/{id}/data")]
        public Task<HttpResponseMessage> PostDataPoint(string id, [Body] Measurement request);

        [Post("/api/v1/sets/{id}/data/batch")]
        public Task<HttpResponseMessage> PostDataPoints(string id, [Body] List<Measurement> request);

        #endregion
    }
}