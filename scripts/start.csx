#!/usr/bin/env dotnet-script

#load ".common.csx"

Run("docker-compose", $"-f docker-compose.yml -f docker-compose.dev.yml build --parallel");
Run("docker-compose", $"-f docker-compose.yml -f docker-compose.dev.yml up -d");
Run("docker-compose", $"-f docker-compose.yml -f docker-compose.dev.yml logs -f --tail 100 etimo-id");
