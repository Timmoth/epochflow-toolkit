# epochflow-toolkit

Repo containing public tools &amp; samples for EpochFlow

## Links
[App](https://app.epochflow.io/) • [Docs](https://docs.epochflow.io/) • [Swagger Api](https://api.epochflow.io/swagger/index.html)

## Nuget package

This [nuget package](https://www.nuget.org/packages/Epochflow/) contains all the classes needed to integrate Epoch flow into your dotnet Api.

### Install the nuget package
```bash
dotnet add package epochflow
```

### Configure services
```csharp
var apiKey = "<api_key>";
var accountId = "<account_id>";
var apiUrl = "<api_url>";
services.AddEpochFlowV1(apiKey, accountId, apiUrl);
```

### Make requests
```csharp
private readonly IEpochFlowV1 _epochflow;

public async Task Load(){
  var setId = "<set_id>";
  var result = await _epochflow.GetSet(setId);
  if(!result.IsSuccessStatusCode){
    return;
  }

  var set = result.Content;
}
```

## Using the CLI toolkit

[Download the latest release for Windows, Linux or Osx](https://github.com/Timmoth/epochflow-toolkit/releases)

### Configuration

You must configure the cli toolkit with the api url, your account id and api key, there are two ways to do this:

#### Parameters

Each command takes the following parameters:

```bash
--url "https://api.epochflow.io/"
--account "<account_id>"
--key "<api_key>"
```

#### Environment variables

Windows:

```powershell
setx epochflow_url "https://api.epochflow.io/"
setx epochflow_account "<account_id>"
setx epochflow_key "<api_key>"

Open a new terminal so the environment variables are updated.
```

Linux / Osx:

```bash
export epochflow_url="https://api.epochflow.io/"
export epochflow_account="<account_id>"
export epochflow_key="<api_key>"
```

### Usage

```bash
# Accounts
./epoch get-account

# Sets
./epoch create-set --name "<set_name>" --collision-mode "<overwrite | combine>"
./epoch list-sets
./epoch get-set --id "<set_id>"
./epoch delete-set --id "<set_id>"

# Tags
./epoch list-tags --id "<set_id>"
./epoch delete-tag --id "<set_id>" --tag "<tag_name>"

# Data
./epoch get-data --id "<set_id>" --tag "<tag_name_1,tag_name_2>"
```
