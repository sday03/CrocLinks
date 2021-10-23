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
    public class GetLinkMetricQueryHandler : IRequestHandler<GetLinkMetricsQuery, LinkMetricModel>
    {
        public readonly CrocLinkSettings _settings;

        public GetLinkMetricQueryHandler(IOptions<CrocLinkSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<LinkMetricModel> Handle(GetLinkMetricsQuery request, CancellationToken cancellationToken)
        {
            IFlurlResponse response = await _settings.ApiHost
                .AllowAnyHttpStatus()
                .AppendPathSegment(_settings.ApiBaseUri)
                .AppendPathSegment("Usage")
                //.WithBasicAuth(...)
                .WithHeader("content-type", "application/json")
                .GetAsync();

            string content = await response.ResponseMessage.Content.ReadAsStringAsync();
            LinkMetricModel model = default(LinkMetricModel);

            if (response.ResponseMessage.IsSuccessStatusCode)
                model = JsonConvert.DeserializeObject<LinkMetricModel>(content);

            return model;
        }
    }
}
