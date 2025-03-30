using API.Configurations;
using Infrastructure.Configurations;
using Application.Configurations;
using Persistence.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("https://localhost:7037") // blazor webassembly address
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAPIServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AllowBlazorClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}
app.UseMiddleware<ConfigureExceptions>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
