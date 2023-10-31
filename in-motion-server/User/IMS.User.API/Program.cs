using IMS.User.BLL;
using IMS.User.DAL;
using IMS.User.Domain;
using IMS.User.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddUserSerilog();

builder.Services.AddUserMassTransit(builder);

builder.Services.AddUserServices();

builder.Services.AddUserMiddlewares();

builder.Services.AddUserValidators();

builder.Services.AddUserRepositories();

builder.Services.AddUserMappers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// using var scope = app.Services.CreateScope();
// var dbContext = scope.ServiceProvider.GetRequiredService<ImsUserDbContext>();
// dbContext.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();