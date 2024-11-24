using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using System.Collections.ObjectModel;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class AddTeamToDancersListOfTeams
    {
        public class AddDancingTeamRequest
        {
            public bool IsTrialLesson { get; set; }
        }

        public static async Task<IResult> AddItemToListOfTeams(SimoneDbContext dbContext,  Guid dancerId, 
            Guid teamId, AddDancingTeamRequest request)
        {
            var teamDataModel = await dbContext.TeamDataModels.FirstOrDefaultAsync(t => t.TeamId == teamId);
            if (teamDataModel == null)
            {
                return TypedResults.NotFound();
            }
            var dancerDataModel = await dbContext.DancerDataModels
               .Include(d => d.TeamDancerRelations)
               .ThenInclude(tr => tr.TeamDataModel)
               .FirstOrDefaultAsync(d => d.DancerId == dancerId);
            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            if (dancerDataModel.TeamDancerRelations == null)
            {
                dancerDataModel.TeamDancerRelations = new List<TeamDancerRelation>();
            }

            var calendarDataModel = await dbContext.CalendarDataModels
            .OrderByDescending(c => c.CreatedDate)
            .FirstOrDefaultAsync();
            if (calendarDataModel == null)
            {
                return TypedResults.NotFound();
            }

            List<DateOnly> danceDates = DanceDatesCalculator.CalculateDanceDates(dbContext, calendarDataModel, teamDataModel);
            var lastDanceDate = danceDates.LastOrDefault();
            if (request.IsTrialLesson == true)
            {
               
                lastDanceDate = danceDates.FirstOrDefault(d => d >= DateOnly.FromDateTime(DateTime.Now));
            }

            dancerDataModel.TeamDancerRelations
                .Add(new TeamDancerRelation
                {
                    TeamId = teamId,
                    DancerId = dancerId,
                    IsTrialLesson = request.IsTrialLesson,
                    LastDanceDate = lastDanceDate

                });
             await dbContext.SaveChangesAsync();    

           var updatedDto = new DancerDto
            {
                DancerId = dancerDataModel.DancerId,
                Name = dancerDataModel.Name,
                TimeOfBirth = dancerDataModel.TimeOfBirth,
                Teams = new Collection<TeamDto>(dancerDataModel.TeamDancerRelations
                        .Select(tdr =>
                        new TeamDto
                        {
                            TeamId = tdr.TeamDataModel.TeamId,
                            Number = tdr.TeamDataModel.Number.ToString(),
                            Name = tdr.TeamDataModel.Name,
                            SceduledTime = tdr.TeamDataModel.ScheduledTime,
                            IsTrialLesson = tdr.IsTrialLesson,
                            LastDancedate = tdr.LastDanceDate
                        }
                        ).ToList())
            };

            return TypedResults.Ok(updatedDto);

        }

        public class DancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateOnly TimeOfBirth { get; set; }
            public IEnumerable<TeamDto> Teams { get; set; } = new List<TeamDto>();
        }

        public class TeamDto 
        {
            public Guid TeamId { get; set; }
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public bool IsTrialLesson { get; set; }
            public DateOnly LastDancedate { get; set; }

        }
    }

}
