[![Build](https://github.com/Etimo/etimo-id/workflows/Build/badge.svg?branch=develop)](https://github.com/Etimo/etimo-id/actions?query=workflow%3ABuild) [![Tests](https://github.com/Etimo/etimo-id/workflows/Tests/badge.svg?branch=develop)](https://github.com/Etimo/etimo-id/actions?query=workflow%3ATests) [![codecov](https://codecov.io/gh/Etimo/etimo-id/branch/develop/graph/badge.svg?token=3TJPDMKNRT)](https://codecov.io/gh/Etimo/etimo-id) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/Etimo/etimo-id/blob/master/LICENSE)

# Etimo ID

Etimo ID is a basic implementation of [OAuth2](https://tools.ietf.org/html/rfc6749#section-5.2), without all the bloat.

At a later stage, [OpenID Connect](https://openid.net/specs/openid-connect-core-1_0.html) will also be implemented.

## Prerequisites

You need both [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) and [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0).

You also need [Docker Desktop](https://www.docker.com/products/docker-desktop) if you plan on running this in Docker.

To install the required dotnet tools, type:

```
dotnet tool restore
```

## Generate private & public key pairs

To generate private & public key pairs, use the `generate-keys.csx` script:

```
dotnet script scripts/generate-keys.csx --output user-secrets
```

This will add private and public keys to your user-secrets.

## Database connection string

Setup your connection string by using `dotnet user-secrets`:

```
dotnet user-secrets --project src/Etimo.Id.Api set ConnectionStrings:EtimoId <CONNECTIONSTRING>
```

## Setting up database

Start the database server by typing `docker-compose -f docker-compose.db.yml up -d`

You can access the database GUI from https://localhost:8011

## Building / running

Run etimo-id by typing `dotnet run --project Etimo.Id.Api`

Or by using the `run.csx` script.

The project is served from https://localhost:5011

## Debugging

In VSCode, simply press `F5` to start a debugging session (it should use the `.NET Core Run` debugger).

## Docker

You can use the `dev.csx` script to build and run etimo-id in a Docker container.

It has debugging enabled, so you can simply attach to the process using the `.NET Core Attach Docker` debugger in VSCode.

This will open a list of processes. Select `Etimo.Id.Api` from the list of processes.

Now you can set breakpoints in the code and debug as usual. It is a bit slower than non-container debugging.

The project is served from https://localhost:5011

### Creating your first users

When starting etimo-id for the first time, a default user and application is added:

```
username: admin
password: etimo
client_id: 11111111-1111-1111-1111-111111111111
client_secret: etimo
```

Use this user to setup the system. When you are done setting up, you should delete this user.

If you want to remove the database and start over, use the `delete-database.csx` script.

## Formatting

View the `.editorconfig` file for a full list of formatting rules.

To install a pre-commit hook that lints the code before committing, use the `install-hooks.csx` script.

## Commit style

This project uses [conventional commits](https://www.conventionalcommits.org/en/v1.0.0/) for its commit messages.

The project uses `husky` + `commitlint` to validate commit messages. Type `npm install` to install these.

### Valid commit types

Type | Description
--- | ---
chore | Adding/updating scripts, configs, etc; no code change
ci | Changes to continuous integration
docs | Changes to documentation
feat | New feature
fix | Bug fix
refactor | Code refactoring
repo | Updates to e.g. git hooks
scripts | Updates to scripts in repository
test | Write and refactor tests; no code change
wip | Work in progress -- use in feature branches where you squash merge

## Helper scripts

Etimo-id comes with a bunch of helper scripts that make development easier.

Run the scripts by issuing e.g. `dotnet script ./scripts/dev.csx`

Script | Description
--- | ---
`add-migration.csx` | Adds a migration file (for pending entity updates).
`bash.csx` | Bash into a running `etimo-id` container.
`build.csx` | Build a `prod` Docker image.
`code-cleanup.csx` | Format the code of modified files.
`delete-database.csx` | Delete the database (and recreate it).
`dev.csx` | Run the application in Docker with hot reload on code changes.
`install-hooks.csx` | Install pre-commit git hooks (code cleanup).
`logs.csx` | Follow the logs of the `etimo-id` container.
`revert-migration.csx` | Revert the latest database migration.
`run.csx` | Start the application (on the host).
`setup-secrets.csx` | Setup secrets (on the host).
`start.csx` | Start the application in `prod` mode in Docker.
`stop.csx` | Stop the `etimo-id` container.
`support.csx` | Start the supportive services (db + db admin tool).
`test.csx` | Run unit tests (on the host) just like the test workflow.
`update-database.csx` | Update the database to the latest migration.
`watch-run.csx` | Start the application (on the host) with hot reload.
`watch-test.csx` | Run the tests (on the host) with hot reload.

💡 Pro tip: create an alias for `dotnet script`, e.g. `ds` to make it easier to start scripts.<br />
💡 Pro tip #2: use tab-completion, e.g. `ds sc<tab>a<tab>` for `scripts/add-migration.csx`
test
test
