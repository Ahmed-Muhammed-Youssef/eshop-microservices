using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();
// Configure Pipeline
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}
// Configure Pipeline
app.UseExceptionHandler(options => { });

app.MapCarter();
app.Run();
