using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CrocLinks.API.Features.LinkShortener.Models;
using CrocLinks.API.Features.LinkShortener.Requests;

namespace CrocLinks.API.Features.LinkShortener.Handlers
{
    public class GetUrlByTokenQueryHandler : IRequestHandler<GetUrlByTokenQuery, Link>
    {
        private readonly LinkShortenerContext _db;

        public GetUrlByTokenQueryHandler(LinkShortenerContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<Link> Handle(GetUrlByTokenQuery request, CancellationToken cancellationToken)
        {
            using (_db)
            {
                Link link = await _db.Links.SingleOrDefaultAsync(x => x.LinkToken == request.Token);

                if(link != null)
                {
                    LinkUsage usage = new LinkUsage()
                    {
                        LinkClickedDate = DateTime.Now,
                        LinkId = link.LinkId
                    };

                    await _db.LinkUsages.AddAsync(usage, cancellationToken);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                return link;
            }
        }
    }
}
