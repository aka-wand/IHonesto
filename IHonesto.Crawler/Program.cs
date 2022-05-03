using IHonesto.Core.Data;
using IHonesto.Core.Data.Repository;
using IHonesto.Crawler;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<DbSession>();
        services.AddTransient<PlataformRepository>();
        services.AddTransient<ProductPlataformRepository>();
        services.AddTransient<ProductRepository>();
        services.AddTransient<StrategyRepository>();
        services.AddTransient<ScanRepository>();
        services.AddTransient<PersistenceContext>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
