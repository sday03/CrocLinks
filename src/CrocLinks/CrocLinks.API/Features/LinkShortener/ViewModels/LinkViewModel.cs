using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocLinks.API.Features.LinkShortener.ViewModels
{
    public class LinkViewModel
    {
        public string Token { get; set; }
        public string OriginalUrl { get; set; }
    }
}
