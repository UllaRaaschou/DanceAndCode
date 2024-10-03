using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;


namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class DeleteDancer
    {
        //TODO: change to WebApplication
        public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapDelete("/dancers", Delete);
        }

        //TODO:ADDED [FromBody]
        public static async Task<IResult> Delete(SimoneDbContext dbContext, 
            IMapper mapper, Guid dancerId)
        {
            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == dancerId);
            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            dbContext.DancerDataModels.Remove(dancerDataModel);
            await dbContext.SaveChangesAsync();
            return TypedResults.NoContent();

        }

        //public class DancerDto
        //{
        //    public Guid DancerId { get; set; }
        //    public string Name { get; set; } = string.Empty;
        //    public DateTime TimeOfBirth { get; set; }
        //    public IEnumerable<TeamDataDto> Teams { get; set; } = null;
        //}

        public class TeamDataDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; }
            public string Name { get; set; }
        }
    }
}
