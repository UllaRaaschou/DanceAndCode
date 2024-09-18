using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class DeleteTeam
    {
        public static void RegisterTeamsWithGuidEndpoints (this WebApplication endPointRouteBuilder)
        {
            endPointRouteBuilder.MapDelete("", Delete);
        }

        public static async Task<IResult> Delete (SimoneDbContext dbContext, Guid teamId) 
        {
            var team = await dbContext.TeamDataModels.FirstOrDefaultAsync(t => t.TeamId == teamId);
            if (team == null)
            {
                return TypedResults.NotFound();
            }
            dbContext.Remove(team);
            await dbContext.SaveChangesAsync();

            return TypedResults.NoContent();

        }

    }
}
