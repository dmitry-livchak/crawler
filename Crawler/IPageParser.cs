using Crawler.Dto;
using HtmlAgilityPack;

namespace Crawler
{
    public interface IPageParser
    {
        Page Parse(HtmlDocument document);
    }
}