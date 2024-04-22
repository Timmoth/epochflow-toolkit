using EpochFlow.ApiClient.Accounts;
using EpochFlow.ApiClient.Analytics;
using EpochFlow.ApiClient.Events;
using EpochFlow.ApiClient.Events.Pipelines;
using EpochFlow.ApiClient.Events.Pipelines.EventCount;
using EpochFlow.ApiClient.Events.Pipelines.EventState;
using EpochFlow.ApiClient.Events.Sets;
using EpochFlow.ApiClient.Measurements;
using EpochFlow.ApiClient.Measurements.Pipelines;
using EpochFlow.ApiClient.Measurements.Pipelines.MeasurementUpdate;
using EpochFlow.ApiClient.Measurements.Pipelines.SeasonalForecast;
using EpochFlow.ApiClient.Measurements.Pipelines.TrainSeasonalityModel;
using EpochFlow.ApiClient.Measurements.Sets;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Permissions;
using EpochFlow.ApiClient.Projects;
using EpochFlow.ApiClient.Projects.Members;
using EpochFlow.ApiClient.Webhooks;
using Refit;

namespace EpochFlow.ApiClient;

public interface IEpochFlowV1
{
    #region Account

    [Patch("/api/v1/account")]
    public Task<ApiResponse<Account>> UpdateAccount(UpdateAccount request);

    [Get("/api/v1/account")]
    public Task<ApiResponse<Account>> GetAccount();

    [Delete("/api/v1/account")]
    public Task<HttpResponseMessage> DeleteAccount();

    [Get("/api/v1/account/invites")]
    public Task<ApiResponse<List<ProjectMember>>> ListInvites();

    [Post("/api/v1/account/invites/{memberId}/accept")]
    public Task<HttpResponseMessage> AcceptProjectInvite(string memberId);

    [Delete("/api/v1/account/invites/{memberId}")]
    public Task<HttpResponseMessage> RejectProjectInvite(string memberId);

    #endregion

    #region Project

    [Get("/api/v1/projects/{projectId}")]
    public Task<ApiResponse<Project>> GetProject(string projectId);

    [Delete("/api/v1/projects/{projectId}")]
    public Task<HttpResponseMessage> DeleteProject(string projectId);

    [Post("/api/v1/projects")]
    public Task<ApiResponse<Project>> CreateProject([Body] CreateProject request);

    [Patch("/api/v1/projects/{projectId}")]
    public Task<ApiResponse<Project>> UpdateProject(string projectId, [Body] UpdateProject request);

    [Get("/api/v1/projects")]
    public Task<ApiResponse<List<Project>>> ListProjects();

    #region Members

    [Post("/api/v1/projects/{projectId}/members")]
    public Task<ApiResponse<ProjectMember>> InviteProjectMember(string projectId, InviteProjectMember request);

    [Get("/api/v1/projects/{projectId}/members")]
    public Task<ApiResponse<List<ProjectMember>>> ListProjectMembers(string projectId);

    [Patch("/api/v1/projects/{projectId}/members/{memberId}")]
    public Task<ApiResponse<ProjectMember>> UpdateProjectMember(string projectId, string memberId,
        PatchProjectMemberRequest request);

    [Delete("/api/v1/projects/{projectId}/members/{memberId}")]
    public Task<HttpResponseMessage> DeleteProjectMember(string projectId, string memberId);

    #endregion

    #endregion

    #region Api keys

    [Get("/api/v1/projects/{projectId}/keys/{keyName}")]
    public Task<ApiResponse<ApiKey>> GetApiKey(string projectId, string keyName);

    [Delete("/api/v1/projects/{projectId}/keys/{keyName}")]
    public Task<HttpResponseMessage> DeleteApiKey(string projectId, string keyName);

    [Post("/api/v1/projects/{projectId}/keys")]
    public Task<ApiResponse<string>> CreateApiKey(string projectId, [Body] CreateApiKey request);

    [Get("/api/v1/projects/{projectId}/keys")]
    public Task<ApiResponse<List<ApiKey>>> ListApiKeys(string projectId);

    [Post("/api/v1/projects/{projectId}/keys/{keyName}/enable")]
    public Task<HttpResponseMessage> EnableApiKey(string projectId, string keyName);

    [Post("/api/v1/projects/{projectId}/keys/{keyName}/disable")]
    public Task<HttpResponseMessage> DisableApiKey(string projectId, string keyName);

    #endregion

    #region Webhooks

    [Get("/api/v1/projects/{projectId}/webhooks")]
    public Task<ApiResponse<List<Webhook>>> ListWebhooks(string projectId);

    [Delete("/api/v1/projects/{projectId}/webhooks/{webhookId}")]
    public Task<HttpResponseMessage> DeleteWebhook(string projectId, string webhookId);

    [Get("/api/v1/projects/{projectId}/webhooks/{webhookId}")]
    public Task<ApiResponse<Webhook>> GetWebhook(string projectId, string webhookId);

    [Post("/api/v1/projects/{projectId}/webhooks")]
    public Task<ApiResponse<Webhook>> CreateWebhook(string projectId, [Body] CreateWebhook request);

    [Patch("/api/v1/projects/{projectId}/webhooks/{webhookId}")]
    public Task<ApiResponse<Webhook>> UpdateWebhook(string projectId, string webhookId,
        [Body] UpdateWebhook request);

    #endregion

    #region Measurements

    #region Pipelines

    [Get("/api/v1/projects/{projectId}/measurements/pipelines")]
    public Task<ApiResponse<List<MeasurementPipeline>>> ListMeasurementPipelines(string projectId);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines")]
    public Task<ApiResponse<List<MeasurementPipeline>>> ListMeasurementPipelines(string projectId, string setId);

    #region Measurement Update

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update/{id}")]
    public Task<ApiResponse<MeasurementUpdatePipeline>> GetMeasurementUpdatePipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update")]
    public Task<ApiResponse<List<MeasurementUpdatePipeline>>> ListMeasurementUpdatePipeline(string projectId,
        string setId);

    [Delete("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update/{id}")]
    public Task<HttpResponseMessage> DeleteMeasurementUpdatePipeline(string projectId, string setId, string id);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update")]
    public Task<ApiResponse<MeasurementUpdatePipeline>> CreateMeasurementUpdatePipeline(string projectId, string setId,
        [Body] CreateMeasurementSourceUpdatePipeline request);

    [Patch("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update/{id}")]
    public Task<ApiResponse<MeasurementUpdatePipeline>> UpdateMeasurementUpdatePipeline(string projectId, string setId,
        string id, [Body] UpdateMeasurementUpdatePipeline request);

    #endregion

    #region Train Seasonality

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model/{id}/train")]
    public Task<HttpResponseMessage> TrainSeasonalityPipeline(string projectId, string setId, string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model/{id}")]
    public Task<ApiResponse<TrainSeasonalityModelPipeline>> GetTrainSeasonalityPipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model")]
    public Task<ApiResponse<List<TrainSeasonalityModelPipeline>>> ListTrainSeasonalityPipelines(string projectId,
        string setId);

    [Delete("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model/{id}")]
    public Task<HttpResponseMessage> DeleteTrainSeasonalityPipeline(string projectId, string setId, string id);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model")]
    public Task<ApiResponse<TrainSeasonalityModelPipeline>> CreateTrainSeasonalityPipeline(string projectId,
        string setId, [Body] CreateTrainSeasonalityPipeline request);

    [Patch("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model/{id}")]
    public Task<ApiResponse<TrainSeasonalityModelPipeline>> UpdateTrainSeasonalityPipeline(string projectId,
        string setId, string id, [Body] UpdateTrainSeasonalityPipeline request);

    [Get(
        "/api/v1/projects/{projectId}/measurements/{setId}/pipelines/train_seasonal_model/{pipelineId}/seasonality/{filter}")]
    public Task<ApiResponse<SeasonalityResponse>> GetMeasurementSeasonality(string projectId, string setId,
        string pipelineId, string filter);

    #endregion

    #region Seasonal Forecast

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast/{id}/forecast")]
    public Task<HttpResponseMessage> RunForecastPipeline(string projectId, string setId, string id,
        [Query] [AliasAs("start")] long startParam,
        [Query] [AliasAs("end")] long endParam);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast/{id}/train")]
    public Task<HttpResponseMessage> SeasonalForecastPipeline(string projectId, string setId, string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast/{id}")]
    public Task<ApiResponse<SeasonalForecastPipeline>> GetSeasonalForecastPipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast")]
    public Task<ApiResponse<List<SeasonalForecastPipeline>>> ListSeasonalForecastPipelines(string projectId,
        string setId);

    [Delete("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast/{id}")]
    public Task<HttpResponseMessage> DeleteSeasonalForecastPipeline(string projectId, string setId, string id);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast")]
    public Task<ApiResponse<SeasonalForecastPipeline>> CreateSeasonalForecastPipeline(string projectId, string setId,
        [Body] PostSeasonalForecastPipelineRequest request);

    [Patch("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/seasonal_forecast/{id}")]
    public Task<ApiResponse<SeasonalForecastPipeline>> UpdateSeasonalForecastPipeline(string projectId, string setId,
        string id, [Body] PatchSeasonalForecastPipelineRequest request);

    #endregion

    #endregion

    #region Sets

    [Get("/api/v1/projects/{projectId}/measurements/{id}")]
    public Task<ApiResponse<MeasurementSet>> GetMeasurementSet(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/measurements/{id}")]
    public Task<HttpResponseMessage> DeleteMeasurementSet(string projectId, string id);

    [Post("/api/v1/projects/{projectId}/measurements")]
    public Task<ApiResponse<MeasurementSet>>
        CreateMeasurementSet(string projectId, [Body] CreateMeasurementSet request);

    [Patch("/api/v1/projects/{projectId}/measurements/{id}")]
    public Task<ApiResponse<MeasurementSet>> UpdateMeasurementSet(string projectId, string id,
        [Body] UpdateMeasurementSet request);

    [Get("/api/v1/projects/{projectId}/measurements")]
    public Task<ApiResponse<List<MeasurementSet>>> ListMeasurementSets(string projectId);

    #endregion

    #region Tags

    [Get("/api/v1/projects/{projectId}/measurements/{id}/tags")]
    public Task<ApiResponse<List<string>>> ListMeasurementTags(string projectId, string id);

    #endregion

    #region Sources

    [Get("/api/v1/projects/{projectId}/measurements/{id}/sources")]
    public Task<ApiResponse<List<MeasurementSource>>> ListMeasurementSources(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/measurements/{id}/sources")]
    public Task<HttpResponseMessage> DeleteMeasurmentSources(string projectId, string id,
        [Query] [AliasAs("source")] string source);

    #endregion

    #region Data

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/data/import/zip/url")]
    Task<ApiResponse<string>> GetMeasurementZipUploadUrl(string projectId, string setId, CancellationToken cancellationToken = default);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/data/archive/zip")]
    Task<HttpResponseMessage> ZipMeasurementSourceArchive(string projectId, string setId,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/data/archive/zip")]
    Task<ApiResponse<string>> GetMeasurementSourceZipArchive(string projectId, string setId, CancellationToken cancellationToken = default);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/data/archive")]
    Task<HttpResponseMessage> RefreshMeasurementSourceArchive(string projectId, string setId, [Query][AliasAs("name")] string? name = null,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/data/archive")]
    Task<ApiResponse<List<ArchiveUrlResponse>>> GetMeasurementSourceArchive(string projectId, string setId, [Query][AliasAs("name")] string? name = null,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/data/import/url")]
    Task<ApiResponse<string>> GetMeasurementUploadUrl(string projectId, string setId, [Query][AliasAs("name")] string name, CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data")]
    public Task<ApiResponse<List<double[]>>> GetMeasurements(string projectId, string id,
        [Query] GetDataRequest request);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/analytics/predicted")]
    public Task<ApiResponse<List<double[]>>> GetPredictedMeasurements(string projectId, string id,
        [Query] GetDataRequest request);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/analytics/downtime")]
    public Task<ApiResponse<long[]>> GetMeasurementDowntime(string projectId, string id,
        [Query] [AliasAs("start")] long startParam,
        [Query] [AliasAs("end")] long endParam,
        [Query] [AliasAs("source")] string source,
        [Query] [AliasAs("resolution")] QueryResolution resolution
    );

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/sources/total")]
    public Task<ApiResponse<List<MeasurementTotal>>> GetMeasurementSourceTotals(string projectId, string id,
        [Query(CollectionFormat.Multi)]
        [AliasAs("sources")]List<string>? sources = null);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/tags/total")]
    public Task<ApiResponse<List<MeasurementTotal>>> GetMeasurementTagTotals(string projectId, string id,
        [Query(CollectionFormat.Multi)]
        [AliasAs("tags")]List<string>? tags = null);

    [Post("/api/v1/projects/{projectId}/measurements/{id}/data")]
    public Task<HttpResponseMessage> PostMeasurement(string projectId, string id, [Body] Measurement request);

    [Post("/api/v1/projects/{projectId}/measurements/{id}/data/batch")]
    public Task<HttpResponseMessage> PostMeasurements(string projectId, string id, [Body] List<Measurement> request);

    #endregion

    #endregion

    #region Events

    #region Pipelines

    [Get("/api/v1/projects/{projectId}/events/pipelines")]
    public Task<ApiResponse<List<EventPipeline>>> ListEventPipelines(string projectId);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines")]
    public Task<ApiResponse<List<EventPipeline>>> ListEventPipelines(string projectId, string setId);

    #region Event Count

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_count/{id}")]
    public Task<ApiResponse<EventCountPipeline>> GetEventCountPipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_count")]
    public Task<ApiResponse<List<EventCountPipeline>>> ListEventCountPipelines(string projectId,
        string setId);

    [Delete("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_count/{id}")]
    public Task<HttpResponseMessage> DeleteEventCountPipeline(string projectId, string setId, string id);

    [Post("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_count")]
    public Task<ApiResponse<EventCountPipeline>> CreateEventCountPipeline(string projectId,
        string setId, [Body] CreateEventCountPipeline request);

    [Patch("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_count/{id}")]
    public Task<ApiResponse<EventCountPipeline>> UpdateEventCountPipeline(string projectId,
        string setId, string id, [Body] UpdateEventCountPipeline request);

    #endregion

    #region Event State

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state/{id}")]
    public Task<ApiResponse<EventStatePipeline>> GetEventStatePipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state/{id}/states")]
    public Task<ApiResponse<List<EventState>>> ListEventStates(string projectId, string setId,
        string id, [Query(CollectionFormat.Multi)][AliasAs("names")] List<string>? names = null, [Query(CollectionFormat.Multi)][AliasAs("properties")] List<string>? properties = null);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state")]
    public Task<ApiResponse<List<EventStatePipeline>>> ListEventStatePipelines(string projectId,
        string setId);

    [Delete("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state/{id}")]
    public Task<HttpResponseMessage> DeleteEventStatePipeline(string projectId, string setId, string id);

    [Post("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state")]
    public Task<ApiResponse<EventStatePipeline>> CreateEventStatePipeline(string projectId,
        string setId, [Body] CreateEventStatePipeline request);

    [Patch("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state/{id}")]
    public Task<ApiResponse<EventStatePipeline>> UpdateEventStatePipeline(string projectId,
        string setId, string id, [Body] UpdateEventStatePipeline request);

    #endregion

    #endregion

    #region Sets

    [Get("/api/v1/projects/{projectId}/events/{id}")]
    public Task<ApiResponse<EventSet>> GetEventSet(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/events/{id}")]
    public Task<HttpResponseMessage> DeleteEventSet(string projectId, string id);

    [Post("/api/v1/projects/{projectId}/events")]
    public Task<ApiResponse<EventSet>> CreateEventSet(string projectId, [Body] CreateEventSet request);

    [Patch("/api/v1/projects/{projectId}/events/{id}")]
    public Task<ApiResponse<EventSet>> UpdateEventSet(string projectId, string id, [Body] UpdateEventSet request);

    [Get("/api/v1/projects/{projectId}/events")]
    public Task<ApiResponse<List<EventSet>>> ListEventSets(string projectId);

    #endregion

    #region Sources

    [Get("/api/v1/projects/{projectId}/events/{id}/sources")]
    public Task<ApiResponse<List<EventSource>>> ListEventSources(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/events/{id}/sources")]
    public Task<HttpResponseMessage> DeleteEventSource(string projectId, string id,
        [Query] [AliasAs("source")] string source);

    [Get("/api/v1/projects/{projectId}/events/{id}/sources/total")]
    public Task<ApiResponse<List<TagEventTotals>>> GetEventSourceTotal(string projectId, string id);

    #endregion

    #region Tags

    [Get("/api/v1/projects/{projectId}/events/{id}/tags")]
    public Task<ApiResponse<List<string>>> ListEventTags(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/events/{id}/tags")]
    public Task<HttpResponseMessage> DeleteEventTag(string projectId, string id, [Query] [AliasAs("tag")] string tag);

    [Get("/api/v1/projects/{projectId}/events/{id}/tags/total")]
    public Task<ApiResponse<List<TagEventTotals>>> GetEventTagsTotal(string projectId, string id);

    #endregion

    #region Data

    [Get("/api/v1/projects/{projectId}/events/{setId}/data/import/zip/url")]
    Task<ApiResponse<string>> GetEventZipUploadUrl(string projectId, string setId, CancellationToken cancellationToken = default);

    [Post("/api/v1/projects/{projectId}/events/{setId}/data/archive/zip")]
    Task<HttpResponseMessage> ZipEventSourceArchive(string projectId, string setId,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/events/{setId}/data/archive/zip")]
    Task<ApiResponse<string>> GetEventSourceZipArchive(string projectId, string setId, CancellationToken cancellationToken = default);

    [Post("/api/v1/projects/{projectId}/events/{setId}/data/archive")]
    Task<HttpResponseMessage> RefreshEventSourceArchive(string projectId, string setId, [Query][AliasAs("name")] string? name = null,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/events/{setId}/data/archive")]
    Task<ApiResponse<List<ArchiveUrlResponse>>> GetEventSourceArchive(string projectId, string setId, [Query][AliasAs("name")] string? name = null,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/events/{setId}/data/import/url")]
    Task<ApiResponse<string>> GetEventUploadUrl(string projectId, string setId, [Query][AliasAs("name")] string name, CancellationToken cancellationToken = default);

    [Post("/api/v1/projects/{projectId}/events/{id}/data")]
    public Task<HttpResponseMessage> PostEvent(string projectId, string id, [Body] EventDataPoint request);

    [Post("/api/v1/projects/{projectId}/events/{id}/data/batch")]
    public Task<HttpResponseMessage> PostEvents(string projectId, string id, [Body] List<EventDataPoint> request);

    [Delete("/api/v1/projects/{projectId}/events/{id}/types")]
    public Task<HttpResponseMessage> DeleteEventType(string projectId, string id,
        [Query] [AliasAs("event")] string @event);

    [Get("/api/v1/projects/{projectId}/events/{id}/types")]
    public Task<ApiResponse<List<string>>> ListEventTypes(string projectId, string id);

    [Get("/api/v1/projects/{projectId}/events/{id}/types/total")]
    public Task<ApiResponse<List<EventTypeTotals>>> GetEventTypesTotal(string projectId, string id);

    [Get("/api/v1/projects/{projectId}/events/{id}/data/count")]
    public Task<ApiResponse<List<long[]>>> GetEventCounts(string projectId, string id,
        [Query] [AliasAs("event")] string? eventName,
        [Query] [AliasAs("start")] long startParam,
        [Query] [AliasAs("end")] long endParam,
        [Query] [AliasAs("source")] string? source,
        [Query] [AliasAs("resolution")] QueryResolution resolution
    );

    [Get("/api/v1/projects/{projectId}/events/{id}/data/numeric")]
    public Task<ApiResponse<List<double[]>>> GetEventNumericProperty(string projectId, string id,
        [Query][AliasAs("event")] string? eventName,
        [Query][AliasAs("start")] long startParam,
        [Query][AliasAs("end")] long endParam,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        [Query][AliasAs("property")] string property,
        [Query][AliasAs("resolution")] QueryResolution resolution,
        [Query][AliasAs("aggregation")] QueryAggregation aggregation
    );

    [Get("/api/v1/projects/{projectId}/events/{id}/data")]
    public Task<ApiResponse<List<EventDataPoint>>> GetEventData(string projectId, string id,
        [Query][AliasAs("event")] string? eventName,
        [Query][AliasAs("start")] long startParam,
        [Query][AliasAs("end")] long endParam,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag
    );

    #endregion

    #endregion
}