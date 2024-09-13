using AutoMapper;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using static SimoneAPI.Tobe.Features.Dancer.SearchForDancerByName;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class AddItemToDancersListOfTeams
    {
        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("/Dancers/{dancerId}/Teams/{teamsId}", AddItemToListOfTeams);
        }

        public static async Task<IResult> AddItemToListOfTeams(SimoneDbContext dbContext, IMapper mapper, DancerDto dto, Guid teamId)
        {
            var teamDataModel = await dbContext.TeamDataModels.FirstOrDefaultAsync(t => t.TeamId == teamId);
            if (teamDataModel == null)
            {
                return TypedResults.NotFound();
            }
            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == dto.DancerId);
            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }
            dancerDataModel.TeamDancerRelations
                .Add(new TeamDancerRelation
                {
                    TeamId = teamId,
                    DancerId = dto.DancerId

                });
            var updatedDto = mapper.Map<DancerDto>(dancerDataModel);
            return TypedResults.Ok(updatedDto);

        }

        public class DancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public IEnumerable<TeamDataDto> Teams { get; set; } = null;
        }

    }

}
