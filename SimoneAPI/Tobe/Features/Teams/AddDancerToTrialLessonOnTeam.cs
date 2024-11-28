using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class AddDancerToTrialLessonOnTeam
    {
        public static void RegisterTeamsWithGuidEndpoint(IEndpointRouteBuilder endPointRouteBuilder)
        {
            endPointRouteBuilder.MapPut("/addTrialLesson", Put);
        }

        public static async Task<IResult> Put(SimoneDbContext dbContext, IMapper mapper, Guid teamId, Guid dancerId)
        {
            var teamDataModel = await dbContext.TeamDataModels
                .Include(t => t.TeamDancerRelations)
                .FirstOrDefaultAsync(t => t.TeamId == teamId);

            if (teamDataModel == null)
            {
                return TypedResults.NotFound();
            }

            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == dancerId);
            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            teamDataModel.TeamDancerRelations.Add(new DataModels.TeamDancerRelation
            {
                DancerId = dancerDataModel.DancerId,
                TeamId = teamDataModel.TeamId,
                IsTrialLesson = true
            });

            await dbContext.SaveChangesAsync();

            var responce = mapper.Map<ResponceDto>(teamDataModel);

            return TypedResults.Ok(responce);
        }

        public class DansersOnTeamDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
        }

        public class ResponceDto
        {
            public Guid? TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string ScheduledTime { get; set; } = string.Empty;
            public ICollection<DansersOnTeamDto> DancersOnTeam { get; set; } = new HashSet<DansersOnTeamDto>();
        }
    }
}

