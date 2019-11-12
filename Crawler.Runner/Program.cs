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
            collection.AddHttpClient<INavigator, Navigator>();
            
            using (var serviceProvider = collection.BuildServiceProvider())
            {
                var worker = serviceProvider.GetService<IWorker>();
                var page = await worker.Scrape("https://www.microsoft.com");
                
                Console.WriteLine(JsonConvert.SerializeObject(page));
            }
        }
    }
}