# epochflow-toolkit

Repo containing public tools &amp; samples for EpochFlow

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
