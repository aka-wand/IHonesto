using IHonesto.Core.Data;
using IHonesto.Core.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;

namespace IHonesto.Scrapper
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PersistenceContext _context;
        private readonly IConfiguration _configuration;

        private readonly ChromeOptions _webdriverOptions;
        private readonly Uri _webdriverUri;

        public Worker(ILogger<Worker> logger, PersistenceContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
            _webdriverOptions = new ChromeOptions();
            _webdriverOptions.AddArgument("no-sandbox");
            _webdriverUri = new Uri(_configuration.GetConnectionString("WebDriver"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var products = await _context.Products.GetBash();

                foreach (var product in products)
                {
                    await ScanProduct(product);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        internal async Task ScanProduct(Product product)
        {
            var productPlataforms = await _context.ProductPlataform.GetByProduct(product.Id);

            foreach (var productPlataform in productPlataforms)
            {
                var plataform = await _context.Plataform.GetById(productPlataform.PlataformId);
                var strategies = await _context.Strategy.GetByPlataform(plataform.Id);

                var builder = new UriBuilder();

                builder.Host = plataform.Host;
                builder.Path = productPlataform.Path;
                builder.Scheme = "https";

                new Task(async () => await ScanPage(product.Id, builder.Uri, strategies)).Start();
            }
        }

        internal async Task ScanPage(int productId, Uri uri, IEnumerable<Strategy> strategies)
        {
            var scan = new Scan
            {
                CreatedAt = DateTime.Now,
                ProductId = productId,
                Metadados = "{}",
            };

            try
            {
                var price = String.Empty;

                using (var driver = new RemoteWebDriver(_webdriverUri, _webdriverOptions))
                {
                    driver.Url = uri.ToString();

                    foreach (var strategy in strategies)
                    {
                        try
                        {
                            scan.StrategyId = strategy.Id;
                            scan.PlataformId = strategy.PlataformId;

                            switch (strategy.Kind)
                            {
                                case Strategy.StrategyKind.Xpath:
                                    var xpath_element = driver.FindElement(By.XPath(strategy.Parameter));
                                    price = Regex.Match(xpath_element.Text, @"R\$ ?\d{1,3}(\.\d{3})*,\d{2}").ToString();
                                    break;
                                case Strategy.StrategyKind.CssSelector:
                                    var css_selector_element = driver.FindElement(By.CssSelector(strategy.Parameter));
                                    price = Regex.Match(css_selector_element.Text, @"R\$ ?\d{1,3}(\.\d{3})*,\d{2}").ToString();
                                    break;
                            }

                            if (!String.IsNullOrEmpty(price))
                            {
                                scan.Price = price;
                                await _context.Scan.Insert(scan);
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            await _context.Scan.InsertError(new ScanError
                            {
                                CreatedAt = DateTime.Now,
                                Message = ex.ToString(),
                                PlataformId = scan.PlataformId,
                                ProductId = scan.ProductId,
                                StrategyId = scan.StrategyId,
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await _context.Scan.InsertError(new ScanError
                {
                    CreatedAt = DateTime.Now,
                    Message = ex.ToString(),
                    PlataformId = scan.PlataformId,
                    ProductId = scan.ProductId,
                    StrategyId = scan.StrategyId,
                });
            }
        }
    }
}