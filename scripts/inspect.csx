#!/usr/bin/env dotnet-script
#load ".common.csx"

/*
  This script will let you "bash into" a
  container that is failing to start. Good
  for debugging.
*/

var project = "etimo-id-api";
if (Args.Any() && Args.First().ToLower() == "web")
{
  project = "etimo-id-web";
}


Run("docker", $"run -it --rm {project} /bin/sh");
