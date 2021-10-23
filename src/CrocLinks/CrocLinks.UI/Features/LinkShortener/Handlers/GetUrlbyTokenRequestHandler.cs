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
    public class GetUrlbyTokenRequestHandler : IRequestHandler<GetUrlByTokenRequest, LinkModel>
    {
        public readonly CrocLinkSettings _settings;

        public GetUrlbyTokenRequestHandler(IOptions<CrocLinkSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<LinkModel> Handle(GetUrlByTokenRequest request, CancellationToken cancellationToken)
        {
            IFlurlResponse response = await _settings.ApiHost
                .AllowAnyHttpStatus()
                .AppendPathSegment(_settings.ApiBaseUri)
                .AppendPathSegment(request.Token)
                //.WithBasicAuth(...)
                .WithHeader("content-type", "application/json")
                .GetAsync();

            string content = await response.ResponseMessage.Content.ReadAsStringAsync();
            LinkModel model = default(LinkModel);

            if (response.ResponseMessage.IsSuccessStatusCode)
                model = JsonConvert.DeserializeObject<LinkModel>(content);

            return model;
        }
    }
}
