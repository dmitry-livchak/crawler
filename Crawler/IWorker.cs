using System.Threading.Tasks;
using Crawler.Dto;

namespace Crawler
{
    public interface IWorker
    {
        Task<Page> Scrape(string url);
    }
}