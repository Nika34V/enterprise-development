using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar (OpenAPI UI)";
        url.Url = "/scalar";
    })
    .OnResourceReady(async (resource, resourceEvent, cancellationToken) =>
    {
        Process.Start(new ProcessStartInfo("http://localhost:5079/scalar") { UseShellExecute = true });
    });

builder.Build().Run();
