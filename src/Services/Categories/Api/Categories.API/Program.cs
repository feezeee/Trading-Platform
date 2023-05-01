using Categories.DAL.Options;
using System.Reflection;
using Categories.DAL;
using Categories.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var categoryContextOptionsSection = builder.Configuration.GetSection("CategoryContextOptions");
var categoryContextOptions = categoryContextOptionsSection.Get<CategoryContextOptions>();


builder.Services.AddCors();

builder.Services.AddCategoriesDal(categoryContextOptions);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CategoryContext>();
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    t =>
    {
        t.AllowAnyOrigin();
        t.AllowAnyMethod();
        t.AllowAnyHeader();
    });

app.UseAuthorization();

app.MapControllers();

app.Run();
