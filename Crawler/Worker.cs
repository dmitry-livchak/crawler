using System.Threading.Tasks;
using Crawler.Dto;

namespace Crawler
{
    public class Worker
    {
        private readonly INavigator _navigator;
        private readonly IPageParser _pageParser;

        public Worker(INavigator navigator, IPageParser pageParser)
        {
            _navigator = navigator;
            _pageParser = pageParser;
        }

        public async Task<Page> Scrape(string url)
        {
            var html = await _navigator.LoadHtml(url);
            var result = _pageParser.Parse(html);
            
            return result;
        }
    }
}