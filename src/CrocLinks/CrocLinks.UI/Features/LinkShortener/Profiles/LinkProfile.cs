using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using CrocLinks.UI.Features.LinkShortener.Models;
using CrocLinks.UI.Features.LinkShortener.ViewModels;

namespace CrocLinks.UI.Features.LinkShortener.Profiles
{
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<LinkModel, LinkViewModel>();
            CreateMap<LinkMetricModel, LinkMetricViewModel>();
        }
    }
}
