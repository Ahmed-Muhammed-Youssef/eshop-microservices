using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services 
builder.Services.AddCarter();

var app = builder.Build();

// Configure pipeline
app.MapCarter();

app.Run();
