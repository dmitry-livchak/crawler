using System.Linq;
using Crawler.Dto;
using HtmlAgilityPack;

namespace Crawler
{
    public class PageParser
    {
        public Page Parse(HtmlDocument document)
        {
            var hyperlinks = document.DocumentNode.SelectNodes("//a");
            return new Page
            {
                Links = hyperlinks.Select(
                    h => h.GetAttributeValue("href", null))
                    .ToArray()
            };
        }
    }
}