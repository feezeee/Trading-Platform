using System.Reflection;
using Users.Application;
using Users.Models.Options;
using Users.Persistence;
using Users.Persistence.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var authOptionsSection = builder.Configuration.GetSection("AuthOptions");
var userContextOptionsSection = builder.Configuration.GetSection("UserContextOptions");

var authOptions = authOptionsSection.Get<AuthOptions>();
var userContextOptions = userContextOptionsSection.Get<UserContextOptions>();

builder.Services.Configure<AuthOptions>(authOptionsSection);
builder.Services.Configure<UserContextOptions>(userContextOptionsSection);



builder.Services.AddUsersApplicationServices();
builder.Services.AddUsersPersistence(userContextOptions);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


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

app.UseAuthorization();

app.MapControllers();

app.Run();
