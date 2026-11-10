var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar (OpenAPI UI)";
        url.Url = "/scalar";
    });

builder.Build().Run();
