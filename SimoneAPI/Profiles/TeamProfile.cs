using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Dtos.Team;
using SimoneAPI.Entities;
using SimoneAPI.Tobe.Features;
using SimoneAPI.Tobe.Features.Teams;
using static SimoneAPI.Tobe.Features.SearchForTeamByName;

namespace SimoneAPI.Profiles
{
    public class TeamProfile: Profile
    {
        public TeamProfile() 
        {
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

            //CreateMap<TeamDataModel, SearchForTeamByName.SearchTeamResponceDto>()
            //    .ForMember(dest => dest.)


            //TODO: Tilføjet manglende mapping så get->Teams virker.
            CreateMap<TeamDataModel, RequestTeamDto>();


            CreateMap<PostTeamDto, Team>();
            CreateMap<Team, TeamDataModel>();
            CreateMap<TeamDataModel, Team>();
            CreateMap<Team, PostTeamResponseDto>();
            
            

            //CreateMap<TeamDataModel, RequestTeamDto>();

            //CreateMap<DancerDataModel, RequestDancerDto>()
            //.ForMember(dest => dest.RegisteredTeams, opt =>
            //    opt.MapFrom(src =>
            //        src.TeamDancerRelations.Select(tdr => tdr.TeamDataModel)))
            //.ReverseMap();
        }
    }
}
