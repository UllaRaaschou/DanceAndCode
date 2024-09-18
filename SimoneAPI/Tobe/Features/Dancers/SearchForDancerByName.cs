using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class SearchForDancerByName
    {
        //public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        //{
        //    endpointRouteBuilder.MapGet("/Dancers", SearchForDancer).WithTags("Dancers");

        //}

        public static async Task<IResult> SearchForDancer(SimoneDbContext dbContext,
            IMapper mapper, [FromQuery] string name)
        {
            IEnumerable<DancerDataModel> models = (IEnumerable<DancerDataModel>)await dbContext.DancerDataModels
                 .Where(d => d.Name.Contains(name)).ToListAsync();

            if (!models.Any())
            {
                return TypedResults.NotFound();
            }

            var responce = mapper.Map<IEnumerable<SearchDancerResponceDto>>(models);
            return TypedResults.Ok(responce);
        }

        public class DancerDataModel
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public ICollection<TeamDancerRelation>? TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();
        }

        public class TeamDataDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; }
            public string Name { get; set; }
        }

        public class SearchDancerResponceDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public IEnumerable<TeamDataDto> Teams { get; set; } = new List<TeamDataDto>();
            public bool TrialLesson { get; set; } = false;  
        }
    }
}
