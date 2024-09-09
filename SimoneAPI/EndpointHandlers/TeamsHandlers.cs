using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Team;
using SimoneAPI.Entities;

namespace SimoneAPI.EndpointHandlers
{
    public class TeamsHandlers
    {
        public static async Task<IResult> PostTeamAsync(SimoneDbContext dbContext, IMapper mapper, PostTeamDto dto)
        {
            var team = mapper.Map<Team>(dto);
            var dataModel = mapper.Map<TeamDataModel>(team);
            dbContext.TeamDataModels.Add(dataModel);
            await dbContext.SaveChangesAsync();

            var teamResponse = mapper.Map<Team>(dataModel);
            var postTeamResponseDto = mapper.Map<PostTeamResponseDto>(teamResponse);

            //TODO: fixed
            return TypedResults.CreatedAtRoute(
                routeName: "GetTeamAsync",
                routeValues: new { teamId = postTeamResponseDto.TeamId
            },
                value: postTeamResponseDto
                );
   
        }

        public static async Task<Ok<IEnumerable<RequestTeamDto>>> GetTeamsAsync(SimoneDbContext dbContext, 
        IMapper mapper)
        {
            var teams = await dbContext.TeamDataModels.ToListAsync();

            return TypedResults.Ok(mapper.Map<IEnumerable<RequestTeamDto>>(teams));

        }

        public static async Task<Results<NotFound, Ok<RequestTeamDto>>> GetTeamAsync (SimoneDbContext dbContext, 
        IMapper mapper, Guid teamId)
        {
            var team = await dbContext.TeamDataModels.FirstOrDefaultAsync(x => x.TeamId == teamId);

            return team != null
            ? TypedResults.Ok(mapper.Map<RequestTeamDto>(team))
            : TypedResults.NotFound();
        }

    }
}
