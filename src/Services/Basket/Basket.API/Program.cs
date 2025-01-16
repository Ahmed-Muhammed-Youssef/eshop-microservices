using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services 
var programAssemply = typeof(Program).Assembly;
var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection")!;

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

builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = redisConnectionString; });

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(postgresConnectionString)
    .AddRedis(redisConnectionString);

builder.Services.AddGrpcClient<Discount.Grpc.Discount.DiscountClient>(o =>
{
    o.Address = new Uri(builder.Configuration["InternalApis:DiscountGrpc"]!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    return handler;
});

var app = builder.Build();

// Configure pipeline
app.UseExceptionHandler(options => { });

app.MapCarter();

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
