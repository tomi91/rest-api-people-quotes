using AutoMapper;
using RestApi.Core.Entities;
using RestApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<PersonDto, Person>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Quote, QuoteDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName));
            CreateMap<QuoteDto, Quote>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<EditQuoteDto, Quote>();

            
        }
    }
}
