using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using CrocLinks.UI.Features.LinkShortener.Models;

namespace CrocLinks.UI.Features.LinkShortener.Requests
{
    public class GetUrlByTokenRequest : IRequest<LinkModel>
    {
        public string Token { get; set; }
    }
}
