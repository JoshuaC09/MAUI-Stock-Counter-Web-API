using WebApplication1.DatabaseContext;
using WebApplication2;
using WebApplication2.Interfaces;
using WebApplication2.Repository;
using WebApplication2.Security;
using WebApplication2.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the IConfiguration instance which MyDbContextFactory depends on
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register MyDbContextFactory
builder.Services.AddSingleton<MyDbContextFactory>();

// Register ConnectionStringProvider
builder.Services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();

// Register DecryptionService with values from appsettings.json
var encryptionSettings = builder.Configuration.GetSection("EncryptionSettings");
var encryptionKey = encryptionSettings.GetValue<string>("Key");
var saltString = encryptionSettings.GetValue<string>("Salt");

if (encryptionKey == null)
{
    throw new ArgumentNullException(nameof(encryptionKey), "Encryption key must not be null.");
}

if (saltString == null)
{
    throw new ArgumentNullException(nameof(saltString), "Salt must not be null.");
}

var salt = Encoding.UTF8.GetBytes(saltString);

builder.Services.AddSingleton(new DecryptionService(encryptionKey, salt));


// Register the repository and service with a factory for DbContext
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICountSheet, CountSheetService>();
//builder.Services.AddScoped<IItemCount, ItemCountService>();
builder.Services.AddScoped<MyDbContext>(provider =>
{
    var connectionStringProvider = provider.GetRequiredService<IConnectionStringProvider>();
    var dbContextFactory = provider.GetRequiredService<MyDbContextFactory>();
    return dbContextFactory.CreateDbContext(connectionStringProvider.GetConnectionString());
});

// Configure CORS to allow any origin, method, and header
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

app.UseAuthorization();
app.MapControllers();
app.Run();


