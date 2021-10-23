using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AdvancedREI;

using CrocLinks.API.Features.LinkShortener.Models;

namespace CrocLinks.API.Features.LinkShortener.Infrastructure
{
    public class ShortLinkGenerator
    {
        public string GenerateToken(long number)
        {
            // base36 seems to be in common usage for url shorteners
            return Base36.NumberToBase36(number);
        }
    }
}
