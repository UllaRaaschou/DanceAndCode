using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using System.Collections.ObjectModel;
using static SimoneAPI.Tobe.Features.Dancer.SearchForDancerByName;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class AddTeamToDancersListOfTeams
    {
        public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("/Dancers/{dancerId}/Teams/{teamsId}", AddItemToListOfTeams);
        }

        public static async Task<IResult> AddItemToListOfTeams(SimoneDbContext dbContext, IMapper mapper, Guid dancerId, Guid teamId)
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

            dancerDataModel.TeamDancerRelations
                .Add(new TeamDancerRelation
                {
                    TeamId = teamId,
                    DancerId = dancerId,
                    IsTrialLesson = false

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
                            SceduledTime = tdr.TeamDataModel.ScheduledTime
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
            
        }
    }

}
