using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DbCtx = PetConnect.Data.PetConnect;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("MySql")!;
builder.Services.AddDbContext<DbCtx>(o => o.UseMySql(cs, ServerVersion.AutoDetect(cs)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PetConnect API",
        Version = "v1",
        Description = "API do PetConnect (EF Core + MySQL)"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetConnect API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();
