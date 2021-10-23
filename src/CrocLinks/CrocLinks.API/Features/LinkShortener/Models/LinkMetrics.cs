using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocLinks.API.Features.LinkShortener.Models
{
    public class LinkMetric
    {
        public long LinkCount { get; set; }
        public long LinksClicked { get; set; }
    }
}
