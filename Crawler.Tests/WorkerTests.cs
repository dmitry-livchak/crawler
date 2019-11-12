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
            navigator.Setup(n => n.LoadHtml("http://url2")).ReturnsAsync(document);
            
            var worker = new Worker(navigator.Object, new PageParser());

            var page = await worker.Scrape("http://url2");
            Assert.Equal("http://url1", page.Links[0]);
        }
    }
}