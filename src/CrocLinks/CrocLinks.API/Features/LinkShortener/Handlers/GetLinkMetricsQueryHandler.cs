using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CrocLinks.API.Features.LinkShortener.Models;
using CrocLinks.API.Features.LinkShortener.Requests;
using CrocLinks.API.Features.LinkShortener.Infrastructure;

namespace CrocLinks.API.Features.LinkShortener.Handlers
{
    public class GetLinkMetricsQueryHandler : IRequestHandler<GetLinkMetricsQuery, LinkMetric>
    {
        private readonly LinkShortenerContext _db;

        public GetLinkMetricsQueryHandler(LinkShortenerContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<LinkMetric> Handle(GetLinkMetricsQuery request, CancellationToken cancellationToken)
        {
            using (_db)
            {
                int linksCreated = await _db.Links.CountAsync(cancellationToken);
                int linksClicked = await _db.LinkUsages.CountAsync(cancellationToken);

                LinkMetric metrics = new LinkMetric()
                {
                    LinkCount = linksCreated,
                    LinksClicked = linksClicked
                };

                return metrics;
            }
        }
    }
}
