using EpochFlow.ApiClient.Accounts;
using EpochFlow.ApiClient.Events;
using EpochFlow.ApiClient.Events.Pipelines;
using EpochFlow.ApiClient.Events.Pipelines.EventState;
using EpochFlow.ApiClient.Events.Sets;
using EpochFlow.ApiClient.Measurements;
using EpochFlow.ApiClient.Measurements.Pipelines;
using EpochFlow.ApiClient.Measurements.Pipelines.MeasurementForecast;
using EpochFlow.ApiClient.Measurements.Pipelines.MeasurementUpdate;
using EpochFlow.ApiClient.Measurements.Sets;
using EpochFlow.ApiClient.Models;
using EpochFlow.ApiClient.Permissions;
using EpochFlow.ApiClient.Projects;
using EpochFlow.ApiClient.Projects.Members;
using EpochFlow.ApiClient.Utilities;
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
    public Task<ApiResponse<ListResponse<ProjectMember>>> ListInvites();

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
    public Task<ApiResponse<ListResponse<Project>>> ListProjects();

    #region Members

    [Post("/api/v1/projects/{projectId}/members")]
    public Task<ApiResponse<ProjectMember>> InviteProjectMember(string projectId, [Body] InviteProjectMember request);

    [Get("/api/v1/projects/{projectId}/members")]
    public Task<ApiResponse<ListResponse<ProjectMember>>> ListProjectMembers(string projectId);

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
    public Task<ApiResponse<ListResponse<ApiKey>>> ListApiKeys(string projectId);

    [Post("/api/v1/projects/{projectId}/keys/{keyName}/enable")]
    public Task<HttpResponseMessage> EnableApiKey(string projectId, string keyName);

    [Post("/api/v1/projects/{projectId}/keys/{keyName}/disable")]
    public Task<HttpResponseMessage> DisableApiKey(string projectId, string keyName);

    #endregion

    #region Webhooks

    [Get("/api/v1/projects/{projectId}/webhooks")]
    public Task<ApiResponse<ListResponse<Webhook>>> ListWebhooks(string projectId);

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
    public Task<ApiResponse<ListResponse<MeasurementPipeline>>> ListMeasurementPipelines(string projectId);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines")]
    public Task<ApiResponse<ListResponse<MeasurementPipeline>>> ListMeasurementPipelines(string projectId, string setId);
    #region Measurement Update

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update/{id}")]
    public Task<ApiResponse<MeasurementUpdatePipeline>> GetMeasurementUpdatePipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_update")]
    public Task<ApiResponse<ListResponse<MeasurementUpdatePipeline>>> ListMeasurementUpdatePipeline(string projectId,
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

    #region Measurement Forecast

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast")]
    public Task<ApiResponse<MeasurementForecastPipeline>> CreateMeasurementForecastPipeline(string projectId,
        string setId, [Body] CreateMeasurementForecastPipeline request);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast")]
    public Task<ApiResponse<ListResponse<MeasurementForecastPipeline>>> ListMeasurementForecastPipelines(string projectId,
        string setId);

    [Post("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast/{id}/train")]
    public Task<HttpResponseMessage> TrainMeasurementForecastPipeline(string projectId, string setId, string id);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast/{id}/forecast")]
    public Task<ApiResponse<ListResponse<double[]>>> GetForecast(string projectId, string setId, string id,
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("filter_id")] string filterId,
        [Query][AliasAs("filter_type")] MeasurementForecastFilterType filterType);

    [Get("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast/{id}")]
    public Task<ApiResponse<MeasurementForecastPipeline>> GetMeasurementForecastPipeline(string projectId, string setId,
        string id);

    [Delete("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast/{id}")]
    public Task<HttpResponseMessage> DeleteMeasurementForecastPipeline(string projectId, string setId, string id);

    [Patch("/api/v1/projects/{projectId}/measurements/{setId}/pipelines/measurement_forecast/{id}")]
    public Task<ApiResponse<MeasurementForecastPipeline>> UpdateMeasurementForecastPipeline(string projectId,
        string setId, string id, [Body] UpdateMeasurementForecastPipeline request);

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
    public Task<ApiResponse<ListResponse<MeasurementSet>>> ListMeasurementSets(string projectId);

    #endregion

    #region Tags

    [Get("/api/v1/projects/{projectId}/measurements/{id}/tags")]
    public Task<ApiResponse<ListResponse<MeasurementTag>>> ListMeasurementTags(string projectId, string id);

    #endregion

    #region Sources

    [Get("/api/v1/projects/{projectId}/measurements/{id}/sources")]
    public Task<ApiResponse<ListResponse<MeasurementSource>>> ListMeasurementSources(string projectId, string id, [Query, AliasAs("source")] string? source = null);

    [Delete("/api/v1/projects/{projectId}/measurements/{id}/sources")]
    public Task<HttpResponseMessage> DeleteMeasurementSource(string projectId, string id,
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
    Task<ApiResponse<ListResponse<ArchiveUrlResponse>>> GetMeasurementSourceArchive(string projectId, string setId, [Query][AliasAs("name")] string? name = null,
        CancellationToken cancellationToken = default);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data")]
    public Task<ApiResponse<ListResponse<double[]>>> GetMeasurements(string projectId, string id,
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("source")] string source,
        [Query][AliasAs("tag")] string tag,
        [Query][AliasAs("resolution")] QueryResolution resolution,
        [Query][AliasAs("aggregation")] QueryAggregation aggregation,
        [Query][AliasAs("filters")] List<string> filters);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/stats")]
    public Task<ApiResponse<MeasurementStats>> GetMeasurementStats(string projectId, string id,
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag);

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/aggregate/hour_of_day")]
    public Task<ApiResponse<ListResponse<double[]>>> GetHourOfDayAggregate(string projectId, string id, 
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        [Query][AliasAs("aggregation")] QueryAggregation aggregation,
        CancellationToken cancellationToken = default
        );

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/aggregate/hour_of_week")]
    public Task<ApiResponse<ListResponse<double[]>>> GetHourOfWeekAggregate(string projectId, string id,
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        [Query][AliasAs("aggregation")] QueryAggregation aggregation,
        CancellationToken cancellationToken = default
    );

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/aggregate/day_of_week")]
    public Task<ApiResponse<ListResponse<double[]>>> GetDayOfWeekAggregate(string projectId, string id,
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        [Query][AliasAs("aggregation")] QueryAggregation aggregation,
        CancellationToken cancellationToken = default
    );

    [Get("/api/v1/projects/{projectId}/measurements/{id}/data/stl")]
    public Task<ApiResponse<ListResponse<double[]>>> GetStl(string projectId, string id,
        [Query][AliasAs("start")] long start,
        [Query][AliasAs("end")] long end,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        CancellationToken cancellationToken = default
    );

    [Post("/api/v1/projects/{projectId}/measurements/{id}/data")]
    public Task<HttpResponseMessage> PostMeasurement(string projectId, string id, [Body] Measurement request);

    [Post("/api/v1/projects/{projectId}/measurements/{id}/data/batch")]
    public Task<HttpResponseMessage> PostMeasurements(string projectId, string id, [Body] List<Measurement> request);

    #endregion

    #endregion

    #region Events

    #region Pipelines

    [Get("/api/v1/projects/{projectId}/events/pipelines")]
    public Task<ApiResponse<ListResponse<EventPipeline>>> ListEventPipelines(string projectId);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines")]
    public Task<ApiResponse<ListResponse<EventPipeline>>> ListEventPipelines(string projectId, string setId);

    #region Event State

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state/{id}")]
    public Task<ApiResponse<EventStatePipeline>> GetEventStatePipeline(string projectId, string setId,
        string id);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state/{id}/states")]
    public Task<ApiResponse<ListResponse<EventState>>> ListEventStates(string projectId, string setId,
        string id, [Query(CollectionFormat.Multi)][AliasAs("names")] List<string>? names = null, [Query(CollectionFormat.Multi)][AliasAs("properties")] List<string>? properties = null);

    [Get("/api/v1/projects/{projectId}/events/{setId}/pipelines/event_state")]
    public Task<ApiResponse<ListResponse<EventStatePipeline>>> ListEventStatePipelines(string projectId,
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
    public Task<ApiResponse<ListResponse<EventSet>>> ListEventSets(string projectId);

    #endregion

    #region Sources

    [Get("/api/v1/projects/{projectId}/events/{id}/sources")]
    public Task<ApiResponse<ListResponse<EventSource>>> ListEventSources(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/events/{id}/sources")]
    public Task<HttpResponseMessage> DeleteEventSource(string projectId, string id,
        [Query] [AliasAs("source")] string source);

    #endregion

    #region Tags

    [Get("/api/v1/projects/{projectId}/events/{id}/tags")]
    public Task<ApiResponse<ListResponse<string>>> ListEventTags(string projectId, string id);

    [Delete("/api/v1/projects/{projectId}/events/{id}/tags")]
    public Task<HttpResponseMessage> DeleteEventTag(string projectId, string id, [Query] [AliasAs("tag")] string tag);

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
    Task<ApiResponse<ListResponse<ArchiveUrlResponse>>> GetEventSourceArchive(string projectId, string setId, [Query][AliasAs("name")] string? name = null,
        CancellationToken cancellationToken = default);

    [Post("/api/v1/projects/{projectId}/events/{id}/data")]
    public Task<HttpResponseMessage> PostEvent(string projectId, string id, [Body] EventDataPoint request);

    [Post("/api/v1/projects/{projectId}/events/{id}/data/batch")]
    public Task<HttpResponseMessage> PostEvents(string projectId, string id, [Body] List<EventDataPoint> request);

    [Delete("/api/v1/projects/{projectId}/events/{id}/types")]
    public Task<HttpResponseMessage> DeleteEventType(string projectId, string id,
        [Query] [AliasAs("event")] string @event);

    [Get("/api/v1/projects/{projectId}/events/{id}/types")]
    public Task<ApiResponse<ListResponse<string>>> ListEventTypes(string projectId, string id);

    [Get("/api/v1/projects/{projectId}/events/{id}/types/total")]
    public Task<ApiResponse<ListResponse<EventTypeTotals>>> GetEventTypesTotal(string projectId, string id);

    [Get("/api/v1/projects/{projectId}/events/{id}/data/count")]
    public Task<ApiResponse<ListResponse<long[]>>> GetEventCounts(string projectId, string id,
        [Query] [AliasAs("start")] long startParam,
        [Query] [AliasAs("end")] long endParam,
        [Query] [AliasAs("event")] string? eventName,
        [Query] [AliasAs("source")] string? source,
        [Query] [AliasAs("tag")] string? tag,
        [Query] [AliasAs("correlation")] string? correlation,
        [Query] [AliasAs("resolution")] QueryResolution resolution
    );

    [Get("/api/v1/projects/{projectId}/events/{id}/data/numeric")]
    public Task<ApiResponse<ListResponse<double?[]>>> GetEventNumericProperties(string projectId, string id,
        [Query][AliasAs("start")] long startParam,
        [Query][AliasAs("end")] long endParam,
        [Query][AliasAs("event")] string? eventName,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        [Query][AliasAs("correlation")] string? correlation,
        [Query(CollectionFormat.Multi)][AliasAs("properties")] List<string> properties,
        [Query][AliasAs("resolution")] QueryResolution resolution,
        [Query][AliasAs("aggregation")] QueryAggregation aggregation
    );

    [Get("/api/v1/projects/{projectId}/events/{id}/data")]
    public Task<ApiResponse<ListResponse<EventDataPoint>>> GetEventData(string projectId, string id,
        [Query][AliasAs("event")] string? eventName,
        [Query][AliasAs("start")] long startParam,
        [Query][AliasAs("end")] long endParam,
        [Query][AliasAs("source")] string? source,
        [Query][AliasAs("tag")] string? tag,
        [Query][AliasAs("correlation")] string? correlation
    );

    #endregion

    #endregion
}