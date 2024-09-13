using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features
{
    public static class SearchForTeamByName
    {
        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/Teams", Get);

        }

        public static async Task<IResult> Get(SimoneDbContext dbContext,
            IMapper mapper, GetTeamDto dto)
        {
            IEnumerable<TeamDataModel> models = await dbContext.TeamDataModels
                .Include(t => t.TeamDancerRelations)
                .Where(d => d.Name.Contains(dto.Name) || d.Number == dto.Number).ToListAsync();

            if (!models.Any())
            {
                return TypedResults.NotFound();
            }

            var responce = mapper.Map<IEnumerable<GetTeamResponceDto>>(models);
            return TypedResults.Ok(responce);
        }

        public class GetTeamDto
        {
            public string Name { get; set; }
            public int Number {  get; set; }
        }

        public class GetTeamResponceDto
        {
            public Guid TeamId { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Number { get; set; } = 0;            
            public IEnumerable<DancerDto> DancersOnTeam { get; set; } = Enumerable.Empty<DancerDto>();
        }

        public class DancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
           
        }


    }
}