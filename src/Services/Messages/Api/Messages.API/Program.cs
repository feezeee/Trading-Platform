using Messages.DAL;
using Messages.DAL.Configurations;
using System.Reflection;
using Messages.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

//builder.Services.AddPersistence(builder.Configuration.GetSection("DatabaseSettings").Get<MongoDbConfiguration>());
//builder.Services.AddApplicationServices();



builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMessagesDal(builder.Configuration.GetSection("DatabaseSettings").Get<MongoDbConfiguration>());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();


var app = builder.Build();

app.UseCors(t =>
{
    t.AllowAnyHeader();
    t.AllowAnyMethod();
    t.SetIsOriginAllowed(t => true);
    t.AllowCredentials();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseEndpoints(endpoints =>
//{
    
//});
//app.UseAuthorization();

app.MapHub<ChatHub>("/chat");
app.MapControllers();


app.Run();
