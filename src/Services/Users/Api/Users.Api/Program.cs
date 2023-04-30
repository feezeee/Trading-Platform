using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddCors();


builder.Services.AddUsersApplicationServices();
builder.Services.AddUsersPersistence(userContextOptions);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // укзывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = false,
            // будет ли валидироваться потребитель токена
            ValidateAudience = false,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme //ApiKeyScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Type = SecuritySchemeType.ApiKey
            },
            new string[] { }
        }
    });

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();
    dbContext.Database.Migrate();
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Use(async (context, next) =>
{
    await Task.Delay(2000);
    await next.Invoke();
});


app.Run();
