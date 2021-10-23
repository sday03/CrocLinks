using System;
using System.Collections.Generic;

#nullable disable

namespace CrocLinks.API.Features.LinkShortener.Models
{
    public partial class LinkUsage
    {
        public long LinkUsageId { get; set; }
        public long LinkId { get; set; }
        public DateTime LinkClickedDate { get; set; }
    }
}
