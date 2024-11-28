using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class PostTeam
    {
        public static async Task<IResult> Post(SimoneDbContext dbContext, PostTeamDto dto)
        {
            var calendarDataModel = new CalendarDataModel(); 
            var teamDataModel = new TeamDataModel(calendarDataModel)
            {
                Number = int.Parse(dto.Number),
                Name = dto.Name,
                ScheduledTime = dto.ScheduledTime,
                DayOfWeek = dto.DayOfWeek
            };

            dbContext.TeamDataModels.Add(teamDataModel);
            await dbContext.SaveChangesAsync();

            var teamResponceDto = new PostTeamResponceDto
            {
                TeamId = teamDataModel.TeamId,
                Number = teamDataModel.Number.ToString(),
                Name = teamDataModel.Name,
                ScheduledTime = teamDataModel.ScheduledTime,
                DayOfWeek = teamDataModel.DayOfWeek,
                TeamDetailsString = $"Hold {teamDataModel.Number} '{teamDataModel.Name}' - {teamDataModel.ScheduledTime}"
    };

            return TypedResults.Created("/Teams", teamResponceDto);

        }

        public class PostTeamDto
        {
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string ScheduledTime { get; set; } = string.Empty;
            public DayOfWeek DayOfWeek { get; set; } = default;

        }

        public class PostTeamResponceDto
        {
            public Guid TeamId { get; set; }
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string ScheduledTime { get; set; } = string.Empty;
            public DayOfWeek DayOfWeek { get; set; } = default;

            public string TeamDetailsString { get; set; }
        }
    }
}
