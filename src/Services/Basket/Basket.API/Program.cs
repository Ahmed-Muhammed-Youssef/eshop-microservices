using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services 
var programAssemply = typeof(Program).Assembly;
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(programAssemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options => {
    options.Connection(postgresConnectionString);
}).UseLightweightSessions();

builder.Services.AddCarter();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure pipeline
app.UseExceptionHandler(options => { });

app.MapCarter();

app.Run();
