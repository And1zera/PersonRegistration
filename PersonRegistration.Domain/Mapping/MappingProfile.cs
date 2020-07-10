using AutoMapper;
using PersonRegistration.Domain.DTOs;
using PersonRegistration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonRegistration.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonDTO, Person>()
                .ReverseMap();
        }
    }
}
