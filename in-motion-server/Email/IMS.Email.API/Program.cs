using IMS.Email.BLL;
using IMS.Email.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddEmailSerilog();

builder.Services.AddEmailMassTransit(builder);

builder.Services.AddControllers();

builder.Services.AddEmailServices(builder);

builder.Services.AddEmailMiddlewares();

builder.Services.AddEmailValidators();

builder.Services.AddEmailMappers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseEmailMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();