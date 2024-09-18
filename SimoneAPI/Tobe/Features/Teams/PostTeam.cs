using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Dtos.Team;
using SimoneAPI.Entities;
using System.Runtime.CompilerServices;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class PostTeam
    {
        public static void RegisterTeamsEndpoint(this WebApplication endPointRouteBuilder)
        {
            endPointRouteBuilder.MapPost("", Post);
        }

        public static async Task<IResult> Post(SimoneDbContext dbContext, IMapper mapper, PostTeamDto dto)
        {
            var teamDataModel = mapper.Map<TeamDataModel>(dto);
            dbContext.TeamDataModels.Add(teamDataModel);
            await dbContext.SaveChangesAsync();

            var teamResponceDto = mapper.Map<PostTeamResponceDto>(teamDataModel);
            return TypedResults.Created("/Teams", teamResponceDto);

        }

        public class PostTeamDto
        {
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            
        }

        public class PostTeamResponceDto 
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
        }
    }

}
