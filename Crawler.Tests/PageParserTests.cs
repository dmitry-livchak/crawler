using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Xunit;

namespace Crawler.Tests
{
    public class PageParserTests
    {
        [Fact]
        public void PageParser_Detects_Single_Hyperlink()
        {
            var parser = new PageParser();
            var html = "<a href=\"http://url1/\">";
            var document = new HtmlDocument();
            document.LoadHtml(html);
            
            var page = parser.Parse(document);
            
            Assert.Equal("http://url1/", page.Links[0]);
        }
    }
}