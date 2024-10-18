using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Tobe.Features.Dancer;
using static SimoneAPI.Tobe.Features.Dancer.AddTeamToDancersListOfTeams;

namespace SimoneAPI.Tobe.Features.Teams
{
    public class DeleteDancerFromTeam
    {
        public static async Task<IResult> Delete(SimoneDbContext dbContext, IMapper mapper, Guid dancerId, Guid teamId)
        {
            var relation = await dbContext.TeamDancerRelations.FirstOrDefaultAsync(tdr => tdr.DancerId == dancerId && tdr.TeamId == teamId);
            if (relation == null)
            {
                return TypedResults.NotFound();
            }
            dbContext.TeamDancerRelations.Remove(relation);
            await dbContext.SaveChangesAsync();

            TeamDataModel? team = await dbContext.TeamDataModels.Include(t => t.TeamDancerRelations).FirstOrDefaultAsync(t => t.TeamId == teamId);
            if (team != null) 
            {
                DeleteDto teamDto = mapper.Map<DeleteDto>(team);
                return TypedResults.Ok(teamDto);
            }
            
            return TypedResults.NoContent();
        }
    }

    public class DeleteDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; } = 0;
        public IEnumerable<DancerDto> DancersOnTeam { get; set; } = Enumerable.Empty<DancerDto>();
    }
}
