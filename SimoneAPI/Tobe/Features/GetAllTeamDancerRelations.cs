using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;

namespace SimoneAPI.Tobe.Features
{
    public class GetAllTeamDancerRelations
    {
        public static async Task<IResult> Get(SimoneDbContext dbContext, Guid teamId)
        {
            TeamDataModel? team = await dbContext.TeamDataModels
                                .SingleOrDefaultAsync(t => t.TeamId == teamId);

            if (team == null)
            {
                return TypedResults.NotFound();
            }

            List<DateOnly> teamDanceDates = await getDanceDates(dbContext, team);

            var teamDancerRelations = await dbContext.TeamDancerRelations
                        .Include(tdr => tdr.DancerDataModel)
                        .Include(tdr => tdr.Attendances)
                        .Where(tdr => tdr.TeamId == teamId)
                        .ToListAsync();

            List<RelationsDto> relationDtoList = teamDancerRelations.Select(tdr => new RelationsDto
            {
                TeamId = tdr.TeamId,
                DancerId = tdr.DancerId,
                DancerName = tdr.DancerDataModel.Name,
                DancersLastDanceDate = tdr.LastDanceDate,
                Attendances = tdr.Attendances.OrderBy(a => a.Date).ToList()
            }).ToList();

            return TypedResults.Ok(relationDtoList);
        }


        private static async Task<List<DateOnly>> getDanceDates(SimoneDbContext context, TeamDataModel team)
        {
            CalendarDataModel? calendarDataModel = await context.CalendarDataModels
                                                    .OrderByDescending(c => c.CreatedDate)
                                                    .FirstOrDefaultAsync();
            if (calendarDataModel == null)
            {
                return new List<DateOnly>();
            }

            var danceDates = DanceDatesCalculator.CalculateDanceDates(context, calendarDataModel, team);
            return danceDates;
        }
    }


    public class RelationsDto
    {
        public Guid TeamId { get; set; }

        public Guid DancerId { get; set; }
        public string DancerName{ get; set; } = string.Empty;
        public DateOnly DancersLastDanceDate { get; set; }
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
