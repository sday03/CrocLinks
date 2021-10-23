using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using CrocLinks.API.Features.LinkShortener.Models;

namespace CrocLinks.API.Features.LinkShortener.Requests
{
    public class GetUrlByTokenQuery : IRequest<Link>
    {
        public string Token { get; set; }
    }
}
