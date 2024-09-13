using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using static SimoneAPI.Tobe.Features.SearchForDancerByName;

namespace SimoneAPI.Tobe.Features
{
    public static class SearchForTeamByName
    {
        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/Teams", SearchForTeam);

        }

        public static async Task<IResult> SearchForTeam(SimoneDbContext dbContext,
            IMapper mapper, SearchForTeamDto dto)
        {
            IEnumerable<TeamDataModel> models = (IEnumerable<TeamDataModel>)await dbContext.TeamDataModels
                 .Where(d => d.Name.Contains(dto.Name)).ToListAsync();

            if (!models.Any())
            {
                return TypedResults.NotFound();
            }

            var responce = mapper.Map<IEnumerable<SearchTeamResponceDto>>(models);
            return TypedResults.Ok(responce);
        }

        public class SearchForTeamDto 
        {
            public string Name { get; set; }
        }

        public class SearchTeamResponceDto 
        {
            public Guid TeamId { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Number { get; set; } = 0;
        }
    }
}
