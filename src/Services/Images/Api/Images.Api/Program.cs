using Images.Core;
using Images.Infrastructure;
using Images.Infrastructure.Configurations;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
//    .AddDockerSecrets()
//    .AddEnvironmentVariables();

//Environment.ExpandEnvironmentVariables("NextCloudSettings:Url");

// Add services to the container.
builder.Services.Configure<NextCloudSettings>(builder.Configuration.GetSection("MyNextCloudSettings"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructure();
builder.Services.AddCore();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "images")),
    RequestPath = "/images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
