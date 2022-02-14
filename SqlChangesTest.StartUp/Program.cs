using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SqlChangesTest.Schema;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration(config =>
{
    config.Sources.Clear();
    config.AddConfiguration(configuration);
});

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

builder.UseSerilog(Log.Logger);

builder.ConfigureServices((hostBuilder, collection) =>
{
    var connectionString = hostBuilder.Configuration.GetConnectionString("Default");

    collection.AddDbContext<TheContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });
});

var app = builder.Build();

Log.Information("Starting up... ");

using (var serviceScope = app.Services.CreateScope())
{
    using var ctx = serviceScope.ServiceProvider.GetService<TheContext>();

    try
    {
        Migrate(ctx);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error running migrations.");
        Environment.ExitCode = -1;
    }
}

Console.WriteLine("The end!");

static void Migrate(TheContext? context)
{
    if (context == null)
    {
        throw new ArgumentNullException(nameof(context));
    }

    if (!context.Database.CanConnect())
    {
        throw new ArgumentException("Cannot connect to the database!", nameof(context));
    }

    context.Database.Migrate();
}