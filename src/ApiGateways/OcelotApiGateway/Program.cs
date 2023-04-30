using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureAppConfiguration((hostingConfiguration, config) =>
{
    config.AddJsonFile($"ocelot.{hostingConfiguration.HostingEnvironment.EnvironmentName}.json", true, true);

});
builder.Host.ConfigureLogging((hostingContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Services.AddOcelot();
builder.Services.AddCors();

var app = builder.Build();

app.Use(async (context, next) =>
{
    await Task.Delay(2000);
    await next.Invoke();
});

app.UseCors(
    t =>
    {
        t.AllowAnyOrigin();
        t.AllowAnyMethod();
        t.AllowAnyHeader();
    });

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();


app.Run();
