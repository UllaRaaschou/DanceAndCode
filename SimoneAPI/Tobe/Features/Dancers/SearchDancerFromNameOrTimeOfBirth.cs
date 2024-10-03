using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public class SearchDancerFromNameOrTimeOfBirth
    {
        public async static Task<IResult> Search(SimoneDbContext dbContext, IMapper mapper, 
            [FromQuery(Name = "name")] string? name, 
            [FromQuery(Name = "timeOfBirth")] DateOnly? timeOfBirth)
        {
            var dancerModels = await dbContext.DancerDataModels
                .Where(d =>
                   (name != null && d.Name.Contains(name)) ||
                   (timeOfBirth != null && d.TimeOfBirth == timeOfBirth))
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
            public Guid DancerId { get; set; }
            public string Name { get; set; }=string.Empty;
            public string TimeOfBirth { get; set; } = string.Empty;
        }
    }
}
