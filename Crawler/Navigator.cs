using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler
{
    public class Navigator : INavigator
    {
        private readonly HttpClient _httpClient;

        public Navigator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HtmlDocument> LoadHtml(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(request);
            using (var html = await response.Content.ReadAsStreamAsync())
            {
                var result = new HtmlDocument();
                result.Load(html);
                
                return result;
            }
        }
    }
}