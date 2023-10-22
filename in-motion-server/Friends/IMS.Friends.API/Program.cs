using IMS.Friends.BLL;
using IMS.Friends.DAL;
using IMS.Friends.Domain;
using IMS.Friends.Models;
using IMS.Shared.Messaging.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddFriendsSerilog();

builder.Services.AddAuthorization();

builder.Services.AddFriendsMassTransit(builder);

builder.Services.AddFriendsServices();

builder.Services.AddFriendsMiddlewares();

builder.Services.AddFriendsValidators();

builder.Services.AddFriendsRepositories();

builder.Services.AddFriendsMappers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Auto migrations
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ImsFriendsDbContext>();
dbContext.Database.Migrate();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseFriendsMiddlewares();

app.UseSharedAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();