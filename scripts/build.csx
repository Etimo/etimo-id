#!/usr/bin/env dotnet-script
#load ".common.csx"

/*
  This script will build etimo-id Docker images.
  Since it will build the entire Dockerfiles, it will
  also run tests. It's a good way of knowing everything
  will build once deployed. It will not, however, run
  the application. So you will have to run the start.csx
  script to ensure it actually runs.
*/

if (Args.Any() && Args.Any(a => a == "--tag"))
{
  var root = GetRootPath();
  Run($"docker", $"build -f {root}/Dockerfile-api --target prod -t etimo-id-api:latest .");
  Run($"docker", $"build -f {root}/Dockerfile-web --target prod -t etimo-id-web:latest .");
  Run("docker", "tag etimo-id-api:latest docker.pkg.github.com/etimo/etimo-id/etimo-id-api:latest");
  Run("docker", "tag etimo-id-web:latest docker.pkg.github.com/etimo/etimo-id/etimo-id-web:latest");

  if (Args.Any(a => a == "--push"))
  {
    Run("docker", "push docker.pkg.github.com/etimo/etimo-id/etimo-id-api:latest");
    Run("docker", "push docker.pkg.github.com/etimo/etimo-id/etimo-id-web:latest");
  }
}
else
{
  Run("docker-compose", $"build");
}
