using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;

namespace SimoneAPI.Tobe.Features.Attendances
{
    public class GetAttendances
    {
        
        public static async Task<IResult> Get(SimoneDbContext dBContext, Guid teamId, Guid dancerId, DateOnly date)
  
        {
            var relation = await dBContext.TeamDancerRelations              
                .FirstOrDefaultAsync(tdr => tdr.TeamId == teamId && tdr.DancerId == dancerId  );
            if (relation == null)
            {
                return TypedResults.NotFound();
            }
            var attendance = relation.Attendances.FirstOrDefault(a => a.Date == date);
            if (attendance == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(attendance);
        }

        
    }

}
