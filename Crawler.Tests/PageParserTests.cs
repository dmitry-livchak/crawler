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
            var document = LoadHtmlDocument("<a href=\"http://url1/\">");

            var page = parser.Parse(document);
            
            Assert.Equal("http://url1/", page.Links[0]);
        }

        [Fact]
        public void PageParser_Detects_Single_Area_Link()
        {
            var parser = new PageParser();
            var document = LoadHtmlDocument("<area href=\"http://url1/\">");

            var page = parser.Parse(document);
            
            Assert.Equal("http://url1/", page.Links[0]);
        }
        
        private static HtmlDocument LoadHtmlDocument(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
    }
}