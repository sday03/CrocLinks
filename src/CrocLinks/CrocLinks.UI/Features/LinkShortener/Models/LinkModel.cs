using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocLinks.UI.Features.LinkShortener.Models
{
    public class LinkModel
    {
        public string Token { get; set; }
        public string OriginalUrl { get; set; }
    }
}
