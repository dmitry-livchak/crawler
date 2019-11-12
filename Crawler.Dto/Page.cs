using System.Collections.Generic;

namespace Crawler.Dto
{
    public class Page
    {
        public List<string> Links { get; set; } = new List<string>();
        public List<string> Resources { get; set; } = new List<string>();
    }
}