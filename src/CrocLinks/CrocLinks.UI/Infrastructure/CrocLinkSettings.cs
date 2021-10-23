using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocLinks.UI.Infrastructure
{
    public class CrocLinkSettings
    {
        public string ApiHost { get; set; }
        public string ApiBaseUri { get; set; }
        public bool UseProxy { get; set; }
        public string Proxy { get; set; }
    }
}
