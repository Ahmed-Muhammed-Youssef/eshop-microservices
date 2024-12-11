
var builder = WebApplication.CreateBuilder(args);
// Add Services
builder.Services.AddCarter();
builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options => {
    options.Connection(builder.Configuration.GetConnectionString("PostgresConnection")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure Pipeline

app.MapCarter();

app.Run();
