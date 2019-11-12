using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Moq;
using Xunit;

namespace Crawler.Tests
{
    public class WorkerTests
    {
        [Fact]
        public async Task Worker_Loads_Single_Page_Stats()
        {
            var html = "<a href=\"http://url1\">";
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var navigator = new Mock<INavigator>(MockBehavior.Strict);
            navigator.Setup(n => n.LoadHtml(new Uri("http://url2"))).ReturnsAsync(document);
            
            var worker = new Worker(navigator.Object, new PageParser());

            var page = await worker.Scrape(new Uri("http://url2"), 0);
            Assert.Equal("http://url1", page.Links[0]);
        }

        [Theory]
        [InlineData("Count", null, null, "Item count is correct")]
        [InlineData("Sub-resource", "http://sub-url1", "http://image-1", "Sub-item content is loaded")]
        [InlineData("Sub-resource", "http://sub-url2", "http://image-2", "Sub-item content is loaded")]
        public async Task Worker_Loads_Subpages_For_All_Links(string @case, string subLink, string subResource, string _)
        {
            var navigator = new Mock<INavigator>(MockBehavior.Strict);

            SetUpNavigator(navigator, new Uri("http://url1"), "<a href=\"http://sub-url1\"><a href=\"http://sub-url2\">");
            SetUpNavigator(navigator, new Uri("http://sub-url1"), "<img src=\"http://image-1\">");
            SetUpNavigator(navigator, new Uri("http://sub-url2"), "<img src=\"http://image-2\">");

            var worker = new Worker(navigator.Object, new PageParser());

            var page = await worker.Scrape(new Uri("http://url1"), 1);

            switch (@case)
            {
                case "Count":
                    Assert.Equal(2, page.Subpages.Count);
                    break;
                case "Sub-resource":
                    Assert.Equal(subResource, page.Subpages[subLink].Resources[0]);
                    break;
                default:
                    throw new ArgumentException("Unknown test case");
            }
        }

        private static void SetUpNavigator(Mock<INavigator> navigator, Uri uri, string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            navigator.Setup(n => n.LoadHtml(uri)).ReturnsAsync(document);
        }
    }
}