using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using System.Runtime.CompilerServices;


namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class DeleteDancer
    {
        public static void RegisterDancerEndpoint(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapDelete("/dancers", Delete);
        }

        public static async Task<IResult> Delete(SimoneDbContext dbContext, IMapper mapper, DancerDto dto)
        {
            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == dto.DancerId);
            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            dbContext.Remove(dancerDataModel);
            return TypedResults.NoContent();

        }

        public class DancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public IEnumerable<TeamDataDto> Teams { get; set; } = null;
        }

        public class TeamDataDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; }
            public string Name { get; set; }
        }
    }
}
