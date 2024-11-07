
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;


namespace SimoneAPI.Tobe.Features.Teams
{
    public class AddToListOfDancersRequest
    {
        public Guid DancerId { get; set; }
        public bool IsTrialLesson
        {
            get; set;
        }
    }
    public static class AddDancerToTeam
    {
        public static async Task<IResult> Put(SimoneDbContext dbContext, Guid teamId, [FromBody] AddToListOfDancersRequest request)
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

            var calendarDataModel = await dbContext.CalendarDataModels
            .OrderByDescending(c => c.CreatedDate)
            .FirstOrDefaultAsync();
            if (calendarDataModel == null)
            {
                return TypedResults.NotFound();
            }

            List<DateOnly> danceDates = DanceDatesCalculator.CalculateDanceDates(calendarDataModel, teamDataModel);
            var lastDanceDate = danceDates.LastOrDefault();
            if (request.IsTrialLesson == true)
            {

                lastDanceDate = danceDates.FirstOrDefault(d => d >= DateOnly.FromDateTime(DateTime.Now));
            }

            teamDataModel.TeamDancerRelations.Add(new DataModels.TeamDancerRelation
            {
                DancerId = dancerDataModel.DancerId,
                TeamId = teamDataModel.TeamId,
                IsTrialLesson = request.IsTrialLesson,
                LastDanceDate = lastDanceDate
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
                        TimeOfBirth = tdr.DancerDataModel.TimeOfBirth,
                        IsTrialLesson = tdr.IsTrialLesson,
                        LastDanceDate = tdr.LastDanceDate

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
        public DateOnly LastDanceDate { get; set; }

        public bool IsTrialLesson { get; set; } 
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
