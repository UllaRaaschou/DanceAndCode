using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public class TjeckForDancerRegisteredStatus
    {
        public async static Task<IResult> CheckForNOTRegistered(SimoneDbContext dbContext, string name, DateOnly timeOfBirth) 
        {
            var dancer = await dbContext.DancerDataModels
                .FirstOrDefaultAsync(d=>
                    d.Name.Contains(name) && 
                    d.TimeOfBirth == timeOfBirth);
            var isNOTRegistered = dancer == null;
            return TypedResults.Ok(isNOTRegistered);
        }
    }
}

