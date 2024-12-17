
using Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args);
// Add Services
var programAssemply = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(programAssemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(programAssemply);

builder.Services.AddMarten(options => {
    options.Connection(builder.Configuration.GetConnectionString("PostgresConnection")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<InitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure Pipeline
app.UseExceptionHandler(options => { });

app.MapCarter();

app.Run();
