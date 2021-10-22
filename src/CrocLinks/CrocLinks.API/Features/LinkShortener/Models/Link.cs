using System;
using System.Collections.Generic;

#nullable disable

namespace CrocLinks.API.Features.LinkShortener.Models
{
    public partial class Link
    {
        public long LinkId { get; set; }
        public DateTime LinkCreatedDate { get; set; }
        public string LinkToken { get; set; }
        public string OriginalLink { get; set; }
        public string ShortenedLink { get; set; }
        public DateTime? LinkExpiryDate { get; set; }
    }
}
