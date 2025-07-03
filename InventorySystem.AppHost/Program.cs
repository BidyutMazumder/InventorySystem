var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.InventorySystem_API>("inventorysystem-api");

builder.Build().Run();
