using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Attendances
{
    public class GetAttendances
    {
        public void RegisterEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/TeamDancerRelations/Attendances", Get);
        }

        public async Task<IResult> Get(SimoneDbContext dBContext, IMapper mapper, Guid dancerId, Guid teamId)
        {
            var relation = await dBContext.TeamDancerRelations
                .Include(tdr => tdr.Attendances)
                .FirstOrDefaultAsync(tdr => tdr.TeamId == teamId && tdr.DancerId == dancerId);
            if (relation == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(relation.Attendances);
        }

        
    }
}
}
