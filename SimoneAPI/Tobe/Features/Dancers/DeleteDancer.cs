using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;


namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class DeleteDancer
    {
       
      
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

        public class TeamDataDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; }
            public string Name { get; set; }
        }
    }
}
