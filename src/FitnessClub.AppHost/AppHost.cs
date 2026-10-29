var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api");

builder.Build().Run();