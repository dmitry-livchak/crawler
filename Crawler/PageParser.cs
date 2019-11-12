using System.Linq;
using Crawler.Dto;
using HtmlAgilityPack;

namespace Crawler
{
    public class PageParser : IPageParser
    {
        public Page Parse(HtmlDocument document)
        {
            var result = new Page();
            var hyperlinks = document.DocumentNode.SelectNodes("//a[@href]|//area[@href]");
            var imagesAndScripts = document.DocumentNode.SelectNodes("//img[@src]|//script[@src]");
            var stylesheets = document.DocumentNode.SelectNodes("//link[@rel='stylesheet' and @href]");

            if (hyperlinks != null)
            {
                result.Links.AddRange(hyperlinks.Select(
                    h => h.GetAttributeValue("href", null)));
            }
            
            if (imagesAndScripts != null)
            {
                result.Resources.AddRange(imagesAndScripts.Select(
                    h => h.GetAttributeValue("src", null)));
            }

            if (stylesheets != null)
            {
                result.Resources.AddRange(stylesheets.Select(
                    h => h.GetAttributeValue("href", null)));
            }

            return result;
        }
    }
}