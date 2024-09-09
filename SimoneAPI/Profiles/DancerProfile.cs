using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Dtos.Team;
using SimoneAPI.Entities;


namespace SimoneAPI.Profiles
{
    public class DancerProfile: Profile
    {
        public DancerProfile()
        {
            CreateMap<PostDancerDto, Dancer>();
            
            CreateMap<Dancer, DancerDataModel>();

            CreateMap<DancerDataModel, Dancer>();

            CreateMap<Dancer, RequestDancerDto>();                

            CreateMap<Dancer, PostDancerResponseDto>();

            CreateMap<Dancer, RequestDancerDto>();

            CreateMap<UpdateDancerDto, DancerDataModel>();

           
            //CreateMap<PostDancerDto, TeamDancerRelations>()
            //    .ForMember(tdr => tdr.
            
            //(CreateMap<PostDancerDto, DancerDataModels>()
            //    .ForMember(ddm => ddm.TeamDancerRelations, opt =>
            //        opt.MapFrom(pdd => pdd.RegisteredTeams.Select()
            
            
            
            //CreateMap<DancerDataModels, RequestDancerDto>();

            //CreateMap<TeamDataModels, GetTeamDto>()
            //.ForMember(dest => dest.EnrolledDancers, opt =>
            //    opt.MapFrom(src =>
            //        src.TeamDancerRelations.Select(tdr => tdr.DancerDataModels)));

            //CreateMap<DancerDataModels, PostResponceDancerDto>();


            //CreateMap<PostDancerDto, DancerDataModels>();

            //CreateMap<PostTeamDto, TeamDancerRelations>()
            //.ForMember(dest => dest.TeamDataModelId, opt => opt.MapFrom(src => src.TeamId))
            //.ForMember(dest => dest.DancerDataModelId, opt => opt.Ignore());



            //CreateMap<PostTeamDto, TeamDataModels>()
            //.ForMember(dest => dest.TeamDancerRelations, opt =>
            //    opt.MapFrom(src =>
            //        src.EnrolledDancers.All)Cre

        }  
    }
}

