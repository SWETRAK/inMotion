using IMS.User.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.AddUserSerilog();
builder.Services.AddUserMassTransit(builder);

builder.Services.AddUserServices();

builder.Services.AddUserMiddlewares();

builder.Services.AddUserValidators();

//builder.Services.AddUserRepositories();

//builder.Services.AddUserMappers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();