
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;


namespace SimoneAPI.Tobe.Features.Teams
{
    public class DeleteFromListOfDancersRequest 
    {
        public Guid DancerId { get; set; }
    }
    public static class AddDancerToTeam
    {
        public static async Task<IResult> Put(SimoneDbContext dbContext, Guid teamId, [FromBody] DeleteFromListOfDancersRequest request)
        {
            var teamDataModel = await dbContext.TeamDataModels
                .Include(t => t.TeamDancerRelations)
                .ThenInclude(tdr => tdr.DancerDataModel)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);

            if (teamDataModel == null)
            {
                return TypedResults.NotFound();
            }

            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == request.DancerId);
            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            teamDataModel.TeamDancerRelations.Add(new DataModels.TeamDancerRelation
            {
                DancerId = dancerDataModel.DancerId,
                TeamId = teamDataModel.TeamId
            });
            await dbContext.SaveChangesAsync();

            var result = new TeamResponceDto
            {
                TeamId = teamDataModel.TeamId,
                Number = teamDataModel.Number.ToString(),
                Name = teamDataModel.Name,
                ScheduledTime = teamDataModel.ScheduledTime,
                DancersOnTeam = teamDataModel.TeamDancerRelations.Select(tdr =>
                {
                    return new DansersOnTeamDto
                    {
                        DancerId = tdr.DancerDataModel.DancerId,
                        Name = tdr.DancerDataModel.Name,
                        TimeOfBirth = tdr.DancerDataModel.TimeOfBirth
                    };
                }).ToList()
            };

            return TypedResults.Ok(result);
        }
    }        

        

    public class DansersOnTeamDto 
    {
        public Guid DancerId { get; set; }
        public string Name {  get; set; } = string.Empty;
        public DateOnly TimeOfBirth { get; set; }
    }

    public class TeamResponceDto 
    {
        public Guid? TeamId { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ScheduledTime { get; set; } = string.Empty;
        public ICollection<DansersOnTeamDto> DancersOnTeam { get; set; } = new HashSet<DansersOnTeamDto>();
    }
    
}
