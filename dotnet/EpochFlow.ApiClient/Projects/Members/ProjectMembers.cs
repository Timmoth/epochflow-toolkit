using System.Text.Json.Serialization;
using EpochFlow.ApiClient.Permissions;

namespace EpochFlow.ApiClient.Projects.Members;

public enum ProjectRequestStatus
{
    Invited,
    Accepted
}

public class ProjectMember
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("project_id")] public string ProjectId { get; set; }
    [JsonPropertyName("project_name")] public string ProjectName { get; set; }
    [JsonPropertyName("account_id")] public string AccountId { get; set; }
    [JsonPropertyName("account_name")] public string AccountName { get; set; }
    [JsonPropertyName("role")] public ProjectRole Role { get; set; }
    [JsonPropertyName("request_status")] public ProjectRequestStatus RequestStatus { get; set; }
}

public class PatchProjectMemberRequest
{
    [JsonPropertyName("role")] public ProjectRole Role { get; set; }
}

public class InviteProjectMember
{
    [JsonPropertyName("account_id")] public string AccountId { get; set; }

    [JsonPropertyName("role")] public ProjectRole Role { get; set; }
}