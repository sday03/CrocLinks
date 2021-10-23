using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CrocLinks.API.Features.LinkShortener.Models;

namespace CrocLinks.API.Features.LinkShortener.Infrastructure
{
    public static class LinkExtensions
    {
        public static bool IsValidToken(this string token)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwxyz1234567890";

            foreach(char c in token.ToCharArray())
            {
                if (!allowedChars.ToCharArray().Contains(c))
                    return false;
            }

            return true;
        }

        public static DateTime TruncateTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
