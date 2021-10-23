using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MediatR;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;

using CrocLinks.UI.Features.LinkShortener.Models;
using CrocLinks.UI.Features.LinkShortener.Requests;
using CrocLinks.UI.Infrastructure;

namespace CrocLinks.UI.Features.LinkShortener.Handlers
{
    public class CreateShortenedUrlCommandHandler : IRequestHandler<CreateShortenedUrlCommand, LinkModel>
    {
        public readonly CrocLinkSettings _settings;

        public CreateShortenedUrlCommandHandler(IOptions<CrocLinkSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<LinkModel> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            IFlurlResponse response = await _settings.ApiHost
                .AllowAnyHttpStatus()
                .AppendPathSegment(_settings.ApiBaseUri)
                .AppendPathSegment("Create")
                //.WithBasicAuth(...)
                .WithHeader("content-type", "application/json")
                .PostJsonAsync(request);

            string content = await response.ResponseMessage.Content.ReadAsStringAsync();
            LinkModel model = default(LinkModel);

            if(response.ResponseMessage.IsSuccessStatusCode)
                model = JsonConvert.DeserializeObject<LinkModel>(content);

            return model;
        }
    }
}
