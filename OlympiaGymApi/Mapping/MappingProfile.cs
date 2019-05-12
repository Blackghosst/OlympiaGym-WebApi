using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.RestAPI.Dtos;
using OlympiaGymApi.Core.Models;

namespace OlympiaGymApi.Mapping
{
    public class MappingProfile : Profile
    {
        IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        public MappingProfile()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<Member, SaveMemberDto>();
            CreateMap<Membership, MembershipDto>();
            CreateMap<District, KeyValuePairDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<Payment, PaymentDto>();
            //CreateMap<Member, SaveMemberDto>()
            //    //.ForMember(m => m.DistrictId, opt=>opt.MapFrom(m=>m.DistrictId))
            //    .ForMember(m => m.Memberships, opt => opt.MapFrom(m => m.Memberships.Select(ms => ms.Id)));

            //Dtos to Domain
            CreateMap<SaveMemberDto, Member>();
            CreateMap<SaveMembershipDto, Membership>();
            CreateMap<MemberDto, Member>();
            CreateMap<MembershipDto, Membership>()
                .ForMember(ms => ms.Status, opt => opt.Ignore());
            CreateMap<KeyValuePairDto, District>();
            CreateMap<PaymentDto, Payment>();
                


        }
    }
}
