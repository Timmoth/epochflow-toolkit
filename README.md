# epochflow-toolkit

Repo containing public tools &amp; samples for EpochFlow

## Links
[App](https://app.epochflow.io/) • [Docs](https://docs.epochflow.io/) • [Swagger Api](https://api.epochflow.io/swagger/index.html)

## Emulator

This [docker image](https://hub.docker.com/r/aptacode/epochflow-emulator) provides a light weight in memory emulator for the API which can be easily ran locally for development or part of your CICD pipeline for integration tests.

```
docker run -p 8085:8080 aptacode/epochflow:latest
```

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

Alternatively, if you don't want to configure DI
```csharp
var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri(EpochFlowApiUrl);
httpClient.DefaultRequestHeaders.Add("X-API-Key'", EpochFlowApiKey);
httpClient.DefaultRequestHeaders.Add("X-Account-Id", EpochFlowAccountId);

var epochFlowApi = RestService.For<IEpochFlowV1>(httpClient);
var result = await epochFlowApi.PostDataPoints(EpochFlowSetId, measurements);
```

## Using the CLI toolkit

[Download the latest release for Windows, Linux or Osx](https://github.com/Timmoth/epochflow-toolkit/releases)

### Configuration

If you want to target the local emulator specify the '--emulator true' flag.
```bash
./epoch get-account --emulator true
```
If you want to target the real api you must configure the cli toolkit with the api url, your account id and api key, there are two ways to do this:

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

# Api Keys
./epoch create-key --name "<key_name>" --expiry "<datetime>" --admin "<True|False>" --all_set_operations "<Undefined | Read | Write>" --permissions "set_1_id;tag1,tag2;read,write&set_2_id;all;read"
./epoch get-key --id "<key_id>"
./epoch list-keys
./epoch enable-key --id "<key_id>"
./epoch disable-key --id "<key_id>"
./epoch delete-key --id "<key_id>"


```
