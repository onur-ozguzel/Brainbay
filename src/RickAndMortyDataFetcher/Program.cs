using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using RickAndMortyDataFetcher.Data;
using RickAndMortyDataFetcher.Services;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment.EnvironmentName;
        Console.WriteLine($"Running in {env} environment.");

        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        ValidateConfigurations(context);

        services.AddDbContext<RickAndMortyDbContext>(opt =>
        {
            opt.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddHttpClient<ICharacterService, CharacterService>((serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(context.Configuration["ApiBaseAddress"]!);
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    })
    .Build();

Console.WriteLine("Database initialization started.");

using var cancellationTokenSource = new CancellationTokenSource();
CancellationToken cancellationToken = cancellationTokenSource.Token;

await InitializeDatabaseAsync(host, cancellationToken);

Console.WriteLine("Database has been initializated successfully.");

await host.StopAsync(cancellationToken);



static void ValidateConfigurations(HostBuilderContext context)
{
    ValidateConfigurationKey(context.Configuration, "ApiBaseAddress");
    ValidateConfigurationKey(context.Configuration, "ConnectionStrings:DefaultConnection");
}

static void ValidateConfigurationKey(IConfiguration configuration, string key)
{
    var value = configuration[key];

    if (string.IsNullOrWhiteSpace(value))
    {
        throw new InvalidOperationException($"{key} is not configured.");
    }
}

static async Task InitializeDatabaseAsync(IHost host, CancellationToken cancellationToken)
{
    using var scope = host.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<RickAndMortyDbContext>();
    var characterService = scope.ServiceProvider.GetRequiredService<ICharacterService>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    await characterService.GetAndSaveAliveCharactersAsync(cancellationToken);
}
