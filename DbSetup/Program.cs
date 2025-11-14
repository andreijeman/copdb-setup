using DbSetup.Data;
using DbSetup.Data.Context;
using DbSetup.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var seed = scope.ServiceProvider.GetRequiredService<SeedService>();

    await db.Database.EnsureDeletedAsync(); 
    await db.Database.MigrateAsync();
    await seed.SeedAsync();
}

app.Run();