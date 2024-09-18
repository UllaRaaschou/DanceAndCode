using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Team;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class GetTeamById
    {
        public static void RegisterTeamsEndpoint(this WebApplication endPointRouteBuilder)
        {
            endPointRouteBuilder.MapGet("", Get);
        }

        public static async Task<Results<NotFound, Ok<RequestTeamDto>>> Get(SimoneDbContext dbContext,
        IMapper mapper, Guid teamId)
        {
            var team = await dbContext.TeamDataModels.FirstOrDefaultAsync(x => x.TeamId == teamId);

            return team != null
            ? TypedResults.Ok(mapper.Map<RequestTeamDto>(team))
            : TypedResults.NotFound();
        }
    }
}
