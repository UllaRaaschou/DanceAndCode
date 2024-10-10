using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features
{
    public static class SearchForTeamByNameOrNumber

    {
        public static async Task<IResult> Get(SimoneDbContext dbContext,
            IMapper mapper, [FromQuery] String? name, [FromQuery] int? number)
        {
            IEnumerable<TeamDataModel> models = await dbContext.TeamDataModels
                .Include(t => t.TeamDancerRelations)
                
                //.Where(d => name == null || d.TeamDataModel.Name.Contains(name))
                .Where(d => number == 0 || d.Number == number)
                .ToListAsync();
              

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