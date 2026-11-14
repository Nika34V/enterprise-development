using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql");
var database = sqlServer.AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithReference(database)
    .WaitFor(database)
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar (OpenAPI UI)";
        url.Url = "/scalar";
    })
    .OnResourceReady(async (resource, resourceEvent, cancellationToken) =>
    {
        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

        Process.Start(new ProcessStartInfo("http://localhost:5079/scalar") { UseShellExecute = true });
    });

builder.Build().Run();