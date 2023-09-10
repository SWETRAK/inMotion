using IMS.Post.BLL;
using IMS.Post.DAL;
using IMS.Post.Domain;
using IMS.Post.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPostRepositories();

builder.Services.AddPostMiddlewares();

builder.Services.AddPostValidators();

builder.Services.AddPostServices();

builder.Services.AddControllers();
builder.Services.AddPostMappers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Automatic migrations, this should be removed in production version
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ImsPostDbContext>();
dbContext.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();