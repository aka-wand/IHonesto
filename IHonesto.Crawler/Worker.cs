using IHonesto.Core.Data;
using IHonesto.Core.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;

namespace IHonesto.Crawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PersistenceContext _context;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, PersistenceContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var products = await _context.Products.GetBash();

                foreach (var product in products)
                {
                    var uri = new Uri("https://www.kabum.com.br/produto/103431");
                    await ScanPage(uri);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        internal async Task ScanPage(Uri uri)
        {
            var options = new ChromeOptions();
            var remote = new Uri(_configuration.GetConnectionString("WebDriver"));

            using (var driver = new RemoteWebDriver(remote, options))
            {
                driver.Url = uri.ToString();

                
            }
        }
    }
}