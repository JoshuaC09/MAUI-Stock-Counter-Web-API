using WebApplication1.DatabaseContext;
using WebApplication2;
using WebApplication2.Interfaces;
using WebApplication2.Security;
using WebApplication2.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplication2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger to use JWT authentication
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
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
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<MyDbContextFactory>();
builder.Services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();


var encryptionSettings = builder.Configuration.GetSection("EncryptionSettings");
var encryptionKey =AppSettingSecurity.EncryptionKey;
var saltString = AppSettingSecurity.EncryptionSalt;

if (encryptionKey == null)
{
    throw new ArgumentNullException(nameof(encryptionKey), "Encryption key must not be null.");
}

if (saltString == null)
{
    throw new ArgumentNullException(nameof(saltString), "Salt must not be null.");
}

var salt = Encoding.UTF8.GetBytes(saltString);
builder.Services.AddSingleton<ISecurity>(new SecurityService(encryptionKey, salt));

builder.Services.AddScoped<ICountSheet, CountSheetService>();
builder.Services.AddScoped<IItemCount, ItemCountService>();
builder.Services.AddScoped<IEmployee, EmployeeService>();
builder.Services.AddScoped<IItem, ItemService>();

builder.Services.AddScoped<MyDbContext>(provider =>
{
    var connectionStringProvider = provider.GetRequiredService<IConnectionStringProvider>();
    var dbContextFactory = provider.GetRequiredService<MyDbContextFactory>();
    var connectionStringTask = connectionStringProvider.GetConnectionStringAsync();
    connectionStringTask.Wait();
    var connectionString = connectionStringTask.Result;

    return dbContextFactory.CreateDbContext(connectionString);
});


var jwtKey = builder.Configuration["Jwt:Key"] ;
if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException(nameof(jwtKey), "JWT key must not be null.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiUser", policy => policy.RequireAuthenticatedUser());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
