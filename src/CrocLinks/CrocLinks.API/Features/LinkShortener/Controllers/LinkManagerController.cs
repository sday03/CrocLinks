using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;
using AutoMapper;
using Swashbuckle.AspNetCore.Annotations;

using CrocLinks.API.Features.LinkShortener.Models;
using CrocLinks.API.Features.LinkShortener.ViewModels;
using CrocLinks.API.Features.LinkShortener.Requests;
using CrocLinks.API.Features.LinkShortener.Infrastructure;

namespace CrocLinks.API.Features.LinkShortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LinkManagerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public LinkManagerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{Token}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a single link object with a given token", typeof(LinkViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request was invalid or malformed.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A link with the given token was not found.")]
        public async Task<IActionResult> Get([FromRoute] GetUrlByTokenQuery query)
        {
            if (!query.Token.IsValidToken())
                return BadRequest();

            Link response = await _mediator.Send(query);

            if (response == null)
                return NotFound();

            return Ok(_mapper.Map<LinkViewModel>(response));
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerResponse(StatusCodes.Status201Created, "Object created successfully and a single Link objectnis returned", typeof(LinkViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request was invalid or malformed.")]
        public async Task<IActionResult> Post([FromBody] CreateShortenedUrlCommand command)
        {
            if (string.IsNullOrEmpty(command.OriginalUrl))
                return BadRequest();

            Link response = await _mediator.Send(command);

            return Created(response.LinkToken, _mapper.Map<LinkViewModel>(response));
        }

        [HttpGet]
        [Route("Usage")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of link metrics", typeof(List<LinkMetricViewModel>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The request return no metrics")]
        public async Task<IActionResult> Get()
        {
            LinkMetric response = await _mediator.Send(new GetLinkMetricsQuery());

            if (response == null)
                return NoContent();

            return Ok(_mapper.Map<LinkMetricViewModel>(response));
        }

    }
}
