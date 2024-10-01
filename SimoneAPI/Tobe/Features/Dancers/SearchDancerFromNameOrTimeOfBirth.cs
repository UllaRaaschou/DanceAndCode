using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public class SearchDancerFromNameOrTimeOfBirth
    {
        public async static Task<IResult> Search(SimoneDbContext dbContext, IMapper mapper, string? name, DateOnly? timeOfBirth)
        {
            var dancerModels = await dbContext.DancerDataModels
                .Where(d =>
                    d.Name.Contains(name) ||
                    d.TimeOfBirth == timeOfBirth)
                .ToListAsync();

            if (dancerModels == null)
            {
                return TypedResults.NotFound();
            }

            var dancerDtos = dancerModels
                .Select(dm => mapper.Map<ResponceDto>(dm))
                .ToList();
            return TypedResults.Ok(dancerDtos);
        }

        public class ResponceDto 
        {
            public string Name { get; set; }
            public string TimeOfBirth { get; set; }
        }
    }
}
