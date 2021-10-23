using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using CrocLinks.API.Features.LinkShortener.Models;
using CrocLinks.API.Features.LinkShortener.ViewModels;

namespace CrocLinks.API.Features.LinkShortener.Profiles
{
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<Link, LinkViewModel>()
                .ForMember(x => x.Token, x => x.MapFrom(y => y.LinkToken))
                .ForMember(x => x.OriginalUrl, x => x.MapFrom(y => y.OriginalLink));

            CreateMap<LinkMetric, LinkMetricViewModel>();
        }
    }
}
