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
        [SwaggerResponse(StatusCodes.Status408RequestTimeout, "Request to the server has timed out.")]
        public async Task<IActionResult> Get([FromRoute] GetUrlByTokenQuery query)
        {
            Link response = await _mediator.Send(query);

            if (response == null)
                return NotFound();

            return Ok(_mapper.Map<LinkViewModel>(response));
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerResponse(StatusCodes.Status201Created, "Object created successfully and a single Link objectnis returned", typeof(LinkViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request was invalid or malformed.")]
        [SwaggerResponse(StatusCodes.Status408RequestTimeout, "Request to the server has timed out.")]
        public async Task<IActionResult> Post([FromBody] CreateShortenedUrlCommand command)
        {
            Link response = await _mediator.Send(command);

            return Created(response.LinkToken, _mapper.Map<LinkViewModel>(response));
        }
    }
}
