using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

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
                ScheduledTime = dto.SceduledTime,
                DayOfWeek = dto.DayOfWeek
            };

            dbContext.TeamDataModels.Add(teamDataModel);
            await dbContext.SaveChangesAsync();

            var teamResponceDto = new PostTeamResponceDto
            {
                TeamId = teamDataModel.TeamId,
                Number = teamDataModel.Number.ToString(),
                Name = teamDataModel.Name,
                SceduledTime = teamDataModel.ScheduledTime,
                DayOfWeek = teamDataModel.DayOfWeek
            };

            return TypedResults.Created("/Teams", teamResponceDto);

        }

        public class PostTeamDto
        {
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public DayOfWeek DayOfWeek { get; set; } = default;

        }

        public class PostTeamResponceDto
        {
            public Guid TeamId { get; set; }
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public DayOfWeek DayOfWeek { get; set; } = default;
        }
    }
}
