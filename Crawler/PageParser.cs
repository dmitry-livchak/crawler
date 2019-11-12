using System.Linq;
using Crawler.Dto;
using HtmlAgilityPack;

namespace Crawler
{
    public class PageParser
    {
        public Page Parse(HtmlDocument document)
        {
            var result = new Page();
            var hyperlinks = document.DocumentNode.SelectNodes("//a|//area");
            result.Links.AddRange(hyperlinks.Select(
                h => h.GetAttributeValue("href", null)));

            return result;
        }
    }
}