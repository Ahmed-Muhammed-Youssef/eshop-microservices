
var builder = WebApplication.CreateBuilder(args);
// Add Services
var programAssemply = typeof(Program).Assembly;

builder.Services.AddCarter();

builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(programAssemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(programAssemply);

builder.Services.AddMarten(options => {
    options.Connection(builder.Configuration.GetConnectionString("PostgresConnection")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure Pipeline

app.MapCarter();

app.Run();
