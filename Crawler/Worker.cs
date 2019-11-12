using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crawler.Dto;

namespace Crawler
{
    public class Worker : IWorker
    {
        private readonly INavigator _navigator;
        private readonly IPageParser _pageParser;
        private readonly List<string> _visitedLinks = new List<string>();

        public Worker(INavigator navigator, IPageParser pageParser)
        {
            _navigator = navigator;
            _pageParser = pageParser;
        }

        public async Task<Page> Scrape(Uri uri, int recursionDepth)
        {
            _visitedLinks.Add(uri.ToString());
            var html = await _navigator.LoadHtml(uri);
            var result = _pageParser.Parse(html);

            if (recursionDepth > 0)
            {
                foreach (var subLink in result.Links)
                {
                    var subUri = new Uri(uri, subLink);
                    if (!_visitedLinks.Contains(subUri.ToString()) && subUri.Host == uri.Host)
                    {
                        result.Subpages.Add(subLink, await Scrape(subUri, recursionDepth - 1));
                    }
                }
            }

            return result;
        }
    }
}