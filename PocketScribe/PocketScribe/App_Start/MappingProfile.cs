using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PocketScribe.Dtos;
using PocketScribe.Models;

namespace PocketScribe.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Transcript, TranscriptDto>();
            Mapper.CreateMap<TranscriptDto, Transcript>();
        }
    }
}