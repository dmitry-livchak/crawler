using System;
using System.Threading.Tasks;
using Crawler.Dto;

namespace Crawler
{
    public class Worker : IWorker
    {
        private readonly INavigator _navigator;
        private readonly IPageParser _pageParser;

        public Worker(INavigator navigator, IPageParser pageParser)
        {
            _navigator = navigator;
            _pageParser = pageParser;
        }

        public async Task<Page> Scrape(Uri uri, int recursionDepth)
        {
            var html = await _navigator.LoadHtml(uri);
            var result = _pageParser.Parse(html);

            if (recursionDepth > 0)
            {
                foreach (var subLink in result.Links)
                {
                    result.Subpages.Add(subLink, await Scrape(new Uri(uri, subLink), recursionDepth - 1));
                }
            }

            return result;
        }
    }
}