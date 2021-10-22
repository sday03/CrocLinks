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
             return Base36.NumberToBase36(number);
        }
    }
}
