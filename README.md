# epochflow-toolkit

Repo containing public tools &amp; samples for EpochFlow

## Using the CLI toolkit

[Download the latest release for Windows, Linux or Osx](https://github.com/Timmoth/epochflow-toolkit/releases)

### Configure environment variables

```
Windows:
setx epochflow_url "https://api.epochflow.io/"
setx epochflow_account "<account id>"
setx epochflow_key "<key>"

Open a new terminal so the environment variables are updated.

Linux / Osx:
export epochflow_url="https://api.epochflow.io/"
export epochflow_account="<account id>"
export epochflow_key="<key>"
```

### Usage

```
./epoch create-set --name "<set_name>" --collision-mode "<overwrite | combine>"
./epoch list-sets
./epoch delete-set --id "<set_id>"
```
