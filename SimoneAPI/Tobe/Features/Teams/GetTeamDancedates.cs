using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Teams
{
    public class GetTeamDancedates
    {
        public static async Task<IResult> Get(SimoneDbContext dbContext, Guid teamId)
        {
            var team = await dbContext.TeamDataModels
                        .FirstOrDefaultAsync(tdm => tdm.TeamId == teamId);

            CalendarDataModel? calendarDataModel = dbContext.CalendarDataModels
                                                    .OrderByDescending(c => c.CreatedDate)
                                                    .FirstOrDefault();

            List<DateOnly> danceDates = new List<DateOnly>();

            if (calendarDataModel != null && team != null)
            {
                danceDates = DanceDatesCalculator.CalculateDanceDates(dbContext, calendarDataModel, team);
            }

            return TypedResults.Ok(danceDates);
        }
    }
}
