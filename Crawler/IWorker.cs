using System;
using System.Threading.Tasks;
using Crawler.Dto;

namespace Crawler
{
    public interface IWorker
    {
        Task<Page> Scrape(Uri uri, int recursionDepth);
    }
}