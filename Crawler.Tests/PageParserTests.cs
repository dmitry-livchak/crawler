using System;
using System.Runtime.InteropServices.ComTypes;
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

        [Theory]
        [InlineData(0, "http://url1/")]
        [InlineData(1, "http://url2/")]
        [InlineData(2, "http://url3/")]
        [InlineData(3, "http://url4/")]
        public void PageParser_Detects_Multiple_Links(int index, string link)
        {
            var parser = new PageParser();
            var document = LoadHtmlDocument(
                "<area href=\"http://url1/\"> <a href=\"http://url2/\">"+
                " <a href=\"http://url3/\"> <area href=\"http://url4/\">");

            var page = parser.Parse(document);
            
            Assert.Equal(link, page.Links[index]);
        }
        
        [Theory]
        [InlineData(0, "Links are empty")]
        [InlineData(1, "Resources are empty")]
        public void PageParser_Parses_Empty_Document(int @case, string _)
        {
            var parser = new PageParser();
            var document = LoadHtmlDocument(string.Empty);

            var page = parser.Parse(document);

            switch (@case)
            {
                case 0:
                    Assert.Empty(page.Links);
                    break;
                case 1:
                    Assert.Empty(page.Resources);
                    break;
                default:
                    throw new ArgumentException("Unknown case");
            }
            
        }

        private static HtmlDocument LoadHtmlDocument(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
        
        

    }
}