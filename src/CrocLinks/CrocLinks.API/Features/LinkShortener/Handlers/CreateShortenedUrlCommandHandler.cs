using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;

using CrocLinks.API.Features.LinkShortener.Models;
using CrocLinks.API.Features.LinkShortener.Requests;
using CrocLinks.API.Features.LinkShortener.Infrastructure;

namespace CrocLinks.API.Features.LinkShortener.Handlers
{
    public class CreateShortenedUrlCommandHandler : IRequestHandler<CreateShortenedUrlCommand, Link>
    {
        private readonly LinkShortenerContext _db;
        private readonly ShortLinkGenerator _generator;

        public CreateShortenedUrlCommandHandler(LinkShortenerContext dbContext, ShortLinkGenerator generator)
        {
            _db = dbContext;
            _generator = generator;
        }

        public async Task<Link> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            using (_db)
            using (IDbCommand command = _db.Database.GetDbConnection().CreateCommand())
            {

                Link link = await _db.Links.SingleOrDefaultAsync(x => x.OriginalLink == request.OriginalUrl, cancellationToken);

                if (link == null)
                {

                    await _db.Database.OpenConnectionAsync();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = @"dbo.GetNextLinkID";
                    long id = (long)command.ExecuteScalar();

                    string token = _generator.GenerateToken(id);

                    link = new Link()
                    {
                        LinkId = id,
                        LinkCreatedDate = DateTime.Now,
                        LinkToken = token,
                        OriginalLink = request.OriginalUrl,
                        ShortenedLink = "/" + token
                    };

                    await _db.Links.AddAsync(link);
                    await _db.SaveChangesAsync();
                }

                return link;
            }
        }
    }
}
