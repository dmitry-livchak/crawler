using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler
{
    public class Navigator : INavigator
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Navigator(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HtmlDocument> LoadHtml(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await httpClient.SendAsync(request);
                using (var html = await response.Content.ReadAsStreamAsync())
                {
                    var result = new HtmlDocument();
                    result.Load(html);

                    return result;
                }
            }
        }
    }
}