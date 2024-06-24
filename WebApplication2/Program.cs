using WebApplication1.DatabaseContext;
using WebApplication2;
using WebApplication2.Interfaces;
using WebApplication2.Security;
using WebApplication2.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<MyDbContextFactory>();
builder.Services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();

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


