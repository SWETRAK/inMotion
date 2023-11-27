using IMS.Post.BLL;
using IMS.Post.DAL;
using IMS.Post.Models;
using IMS.Shared.Messaging.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddPostSerilog();

builder.Services.AddPostRepositories();

builder.Services.AddPostMassTransit(builder);

builder.Services.AddPostMiddlewares();

builder.Services.AddPostValidators();

builder.Services.AddPostServices();

builder.Services.AddPostMappers();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UsePostMiddlewares();

app.UseSharedAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();