using System;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler
{
    public interface INavigator
    {
        Task<HtmlDocument> LoadHtml(Uri uri);
    }
}