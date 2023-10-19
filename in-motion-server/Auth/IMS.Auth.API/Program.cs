using IMS.Auth.BLL;
using IMS.Auth.BLL.Authentication;
using IMS.Auth.DAL;
using IMS.Auth.Domain;
using IMS.Auth.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddAuthSerilog();

builder.Services.AddAuthMassTransit(builder);

builder.Services.AddControllers();

builder.Services.AddAuthServices();

builder.Services.AddAuthAuthentication(builder);

builder.Services.AddAuthMiddlewares();

builder.Services.AddAuthValidators();

builder.Services.AddAuthRepositories();

builder.Services.AddAuthMappers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ImsAuthDbContext>();
dbContext.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();