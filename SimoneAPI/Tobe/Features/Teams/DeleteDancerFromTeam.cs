
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Tobe.Features.Dancer;
using static SimoneAPI.Tobe.Features.Dancer.AddTeamToDancersListOfTeams;
using static SimoneAPI.Tobe.Features.Dancer.SearchForDancerByName;

namespace SimoneAPI.Tobe.Features.Teams
{
    public class DeleteDancerFromTeam
    {
        public static async Task<IResult> Delete(SimoneDbContext dbContext, Guid teamId, [FromBody] DeleteFromListOfDancersRequest request)
        {
            var relation = await dbContext.TeamDancerRelations.FirstOrDefaultAsync(tdr => tdr.DancerId == request.DancerId && tdr.TeamId == teamId);
            if (relation == null)
            {
                return TypedResults.NotFound();
            }
            dbContext.TeamDancerRelations.Remove(relation);
            await dbContext.SaveChangesAsync();

            TeamDataModel? teamDataModel = await dbContext.TeamDataModels
                .Include(t => t.TeamDancerRelations)
                .ThenInclude(tdr => tdr.DancerDataModel)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
            if (teamDataModel != null) 
            {
                var result = new TeamResponceDto
                {
                    TeamId = teamDataModel.TeamId,
                    Number = teamDataModel.Number.ToString(),
                    Name = teamDataModel.Name,
                    ScheduledTime = teamDataModel.ScheduledTime,
                    DancersOnTeam = teamDataModel.TeamDancerRelations.Select(tdr => new DansersOnTeamDto
                    {
                        DancerId = tdr.DancerDataModel.DancerId,
                        Name = tdr.DancerDataModel.Name,
                        TimeOfBirth = tdr.DancerDataModel.TimeOfBirth
                    }).ToList()
                };
                return TypedResults.Ok(result);
            }
            
            return TypedResults.NoContent();
        }
    }

    public class DeleteDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public IEnumerable<DancerDto> DancersOnTeam { get; set; } = Enumerable.Empty<DancerDto>();
    }
}
