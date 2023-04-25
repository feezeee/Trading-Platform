using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
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


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.UseCors(
    t =>
    {
        t.AllowAnyHeader();
        t.AllowAnyMethod();
        t.SetIsOriginAllowed(t => true);
        t.AllowCredentials();
    });
app.Run();
