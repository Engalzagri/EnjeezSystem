var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apigateway = builder.AddProject<Projects.EnjeezSystem_ApiGateway>("apiservice");

builder.AddProject<Projects.EnjeezSystem_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apigateway)
    .WaitFor(apigateway);

builder.AddProject<Projects.EnjeezSystem_AuthService>("enjeezsystem-authservice");

builder.Build().Run();
