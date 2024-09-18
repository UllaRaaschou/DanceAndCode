using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Dtos.Team;
using SimoneAPI.Entities;
using SimoneAPI.Tobe.Features;
using SimoneAPI.Tobe.Features.Dancer;
using SimoneAPI.Tobe.Features.Teams;
using static SimoneAPI.Tobe.Features.SearchForTeamByName;

namespace SimoneAPI.Profiles
{
    public class TeamProfile: Profile
    {
        public TeamProfile() 
        {
            CreateMap<PostTeam.PostTeamDto, TeamDataModel>();


            CreateMap<TeamDataModel, PostTeam.PostTeamResponceDto>();
               

            CreateMap<TeamDataModel, GetTeamResponceDto>()
                .ForMember(dest => dest.DancersOnTeam, opt => 
                opt.MapFrom(scr => scr.TeamDancerRelations != null
                ? scr.TeamDancerRelations.Select(tdr => tdr.DancerId)
                : null));

            CreateMap<TeamDataModel, AddDancerToTeam.ResponceDto>()
                .ForMember(dest => dest.DancersOnTeam, opt =>
                opt.MapFrom(scr => scr.TeamDancerRelations != null
                ? scr.TeamDancerRelations.Select(tdr => tdr.DancerId)
                : null));




            CreateMap<TeamDataModel, AddTeamToDancersListOfTeams.TeamDto>()
            .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.TeamId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.SceduledTime, opt => opt.MapFrom(src => src.SceduledTime));







            //TODO: Tilføjet manglende mapping så get->Teams virker.
            CreateMap<TeamDataModel, RequestTeamDto>();
            CreateMap<UpdateTeam.UpdateTeamDto, TeamDataModel>();
            CreateMap<TeamDataModel, UpdateTeam.UpdateTeamResponceDto>();

            //CreateMap<PostTeamDto, Team>();
            //CreateMap<Team, TeamDataModel>();
            //CreateMap<TeamDataModel, Team>();
            //CreateMap<Team, PostTeamResponseDto>();
            
            

            //CreateMap<TeamDataModel, RequestTeamDto>();

            //CreateMap<DancerDataModel, RequestDancerDto>()
            //.ForMember(dest => dest.RegisteredTeams, opt =>
            //    opt.MapFrom(src =>
            //        src.TeamDancerRelations.Select(tdr => tdr.TeamDataModel)))
            //.ReverseMap();
        }
    }
}
