using System.Reflection;
using Amazon.Runtime.Internal.Auth;
using Products.Api;
using Products.Application;
using Products.Persistence;
using Products.Persistence.MongoDatabase.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.AddPersistence(builder.Configuration.GetSection("DatabaseSettings").Get<MongoDbConfiguration>());
builder.Services.AddApplicationServices();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(
    t =>
    {
        t.AllowAnyOrigin();
        t.AllowAnyMethod();
        t.AllowAnyHeader();
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


//app.Use(async (context, next) =>
//{
//    await Task.Delay(2000);
//    await next.Invoke();
//});

app.Run();
