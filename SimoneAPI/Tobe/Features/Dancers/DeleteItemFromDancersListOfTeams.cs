using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using static SimoneAPI.Tobe.Features.Dancer.SearchForDancerByName;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class DeleteItemFromDancersListOfTeams
    {

        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapDelete("/Dancers/{dancerId}/Teams/{teamId}", DeleteItemFromListOfTeams);


        }

        public static async Task<IResult> DeleteItemFromListOfTeams(SimoneDbContext dbContext, IMapper mapper,
            DancerDto dto, Guid teamId)
        {
            var danserDataModel = await dbContext.DancerDataModels
                .Include(d => d.TeamDancerRelations)
                .FirstOrDefaultAsync(d => d.DancerId == dto.DancerId);

            if (danserDataModel != null)
            {
                var teamToRemove = danserDataModel.TeamDancerRelations.FirstOrDefault(tdr => tdr.TeamId == teamId);
                if (teamToRemove != null)
                {
                    danserDataModel.TeamDancerRelations.Remove(teamToRemove);
                    await dbContext.SaveChangesAsync();
                }
            }
            return TypedResults.Ok(dto);
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

