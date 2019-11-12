using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Crawler.Tests
{
    public class NavigatorTests
    {
        [Fact]
        public async Task Navigator_Gets_HtmlDocument_From_Url()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("<html><head><title>Title 1</title></head></html>"),
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var httpClientFactory = new Mock<IHttpClientFactory>(MockBehavior.Strict);
            httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var navigator = new Navigator(httpClientFactory.Object);
            var document = await navigator.LoadHtml(new Uri("http://url1/"));
            
            Assert.Equal("Title 1", document.DocumentNode.SelectSingleNode("//title").InnerText);
        }
    }
}