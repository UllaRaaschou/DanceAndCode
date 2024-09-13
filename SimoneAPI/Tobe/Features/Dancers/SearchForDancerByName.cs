using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class SearchForDancerByName
    {
        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/Dancers", SearchForDancer);

        }

        public static async Task<IResult> SearchForDancer(SimoneDbContext dbContext,
            IMapper mapper, SearchForDancerDto dto)
        {
            IEnumerable<DancerDataModel> models = (IEnumerable<DancerDataModel>)await dbContext.DancerDataModels
                 .Where(d => d.Name.Contains(dto.Name)).ToListAsync();

            if (!models.Any())
            {
                return TypedResults.NotFound();
            }

            var responce = mapper.Map<IEnumerable<SearchDancerResponceDto>>(models);
            return TypedResults.Ok(responce);
        }

        public class SearchForDancerDto
        {
            public string Name { get; set; } = string.Empty;
        }

        public class DancerDataModel
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public ICollection<TeamDancerRelation>? TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();
        }

        //public class TeamDancerRelation
        //{           
        //    public Guid TeamDancerRelationId { get; set; }
        //    public Guid TeamId { get; set; }
        //    public Guid DancerId { get; set; }
        //    public TeamDataModel TeamDataModel { get; set; } = null!;
        //    public DancerDataModel DancerDataModel { get; set; } = null!;
        //}

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
