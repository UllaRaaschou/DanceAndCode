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
            var models =  dbContext.TeamDataModels.Include(t => t.TeamDancerRelations).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(t => t.Name.Contains(name));
            }

            if (number.HasValue)
            {
                models = models.Where(t => t.Number == number);
            }

            var res = await models.ToListAsync();
               
            if (res.Count == 0)
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
            public string SceduledTime { get; set; } = string.Empty;
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