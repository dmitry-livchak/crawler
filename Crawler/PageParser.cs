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
            var images = document.DocumentNode.SelectNodes("//img");
            var stylesheets = document.DocumentNode.SelectNodes("//link[@rel='stylesheet']");

            if (hyperlinks != null)
            {
                result.Links.AddRange(hyperlinks.Select(
                    h => h.GetAttributeValue("href", null)));
            }
            
            if (images != null)
            {
                result.Resources.AddRange(images.Select(
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