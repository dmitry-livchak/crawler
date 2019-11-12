using System.Collections.Generic;

namespace Crawler.Dto
{
    public class Page
    {
        public List<string> Links { get; } = new List<string>();
        public List<string> Resources { get; } = new List<string>();
        public Dictionary<string, Page> Subpages { get; } = new Dictionary<string, Page>();
    }
}