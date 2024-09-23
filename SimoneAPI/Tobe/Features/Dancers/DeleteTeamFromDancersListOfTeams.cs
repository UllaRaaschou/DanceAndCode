using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public static class DeleteTeamFromDancersListOfTeams
    {
        public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapDelete("/Dancers/{dancerId}/Teams/{teamsId}", Delete);
        }

        public static async Task<IResult> Delete(SimoneDbContext dbContext, IMapper mapper, Guid dancerId, Guid teamId)
        {
            var relation = await dbContext.TeamDancerRelations.FirstOrDefaultAsync(tdr => tdr.DancerId == dancerId && tdr.TeamId == teamId);
            if (relation == null)
            {
                return TypedResults.NotFound();
            }
            dbContext.TeamDancerRelations.Remove(relation);
            await dbContext.SaveChangesAsync();
            return TypedResults.NoContent();
        }

        

    }
}


