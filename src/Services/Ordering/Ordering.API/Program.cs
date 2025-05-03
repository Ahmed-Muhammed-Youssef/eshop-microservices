
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);
var app = builder.Build();
// Configure Pipeline
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
// Configure Pipeline
app.UseExceptionHandler(options => { });

app.MapCarter();
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
