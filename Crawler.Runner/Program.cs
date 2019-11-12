using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Crawler.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var collection = new ServiceCollection();
            collection.AddTransient<IPageParser, PageParser>();
            collection.AddTransient<IWorker, Worker>();
            collection.AddTransient<INavigator, Navigator>();
            collection.AddHttpClient();
            
            using (var serviceProvider = collection.BuildServiceProvider())
            {
                var worker = serviceProvider.GetService<IWorker>();
                var page = await worker.Scrape(new Uri("http://wiprodigital.com"), 5);
                
                Console.WriteLine(JsonConvert.SerializeObject(page));
            }
        }
    }
}