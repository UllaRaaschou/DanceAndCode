using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Dtos.Team;
using SimoneAPI.Entities;

namespace SimoneAPI.Profiles
{
    public class TeamProfile: Profile
    {
        public TeamProfile() 
        {
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
