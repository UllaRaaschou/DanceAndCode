using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public static class GetAllDancersOnTeam
    {
        public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("dancers/{dancerId:guid}", Get);
        }
        public static async Task<IResult> Get(SimoneDbContext dbContext, 
            IMapper mapper, Guid teamId)
        {

            var listOfEnrolledDancerDataModels = await dbContext.TeamDancerRelations
                .Where(x => x.TeamId == teamId)
                .Select(x => x.DancerDataModel)                
                .ToListAsync();

            var listOfEnrolledDancers = listOfEnrolledDancerDataModels
            .Select(d => mapper.Map<GetDancerResponceDto>(d))
            .ToList();

            return TypedResults.Ok(listOfEnrolledDancers);

        }

        public class GetDancerResponceDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public IEnumerable<string> Teams { get; set; } = new List<string>();

        }
    }
}
