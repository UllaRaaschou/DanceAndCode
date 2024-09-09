using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Entities;

namespace SimoneAPI.EndpointHandlers
{
    public class DancersHandlers
    {
        public static async Task<IResult> GetDancersAsync(SimoneDbContext dbContext, IMapper mapper, Guid? teamId)
        {

            var listOfEnrolledDancerDataModels = await dbContext.TeamDancerRelations
                .Where(x => x.TeamId == teamId)
                .Include(x => x.DancerDataModel)
                .Select(x => x.DancerDataModel)
                .ToListAsync();
            var listOfEnrolledDancers = listOfEnrolledDancerDataModels
            .Select(d => mapper.Map<Dancer>(d))
            .ToList();
            var listOfEnrolledDancerDtos = listOfEnrolledDancers
            .Select(d => mapper.Map<RequestDancerDto>(d))
            .ToList();

            return TypedResults.Ok(listOfEnrolledDancerDtos);


        }

        public static async Task<IResult> PostDancer(SimoneDbContext dbContext, IMapper mapper, PostDancerDto dto)
        {
            var dancer = mapper.Map<Dancer>(dto);
            var datamodel = mapper.Map<DancerDataModel>(dancer);
            dbContext.DancerDataModels.Add(datamodel);
            await dbContext.SaveChangesAsync();

            var dancerResponse = mapper.Map<Dancer>(datamodel);
            var postDancerResponseDto = mapper.Map<PostDancerResponseDto>(dancerResponse);

            return TypedResults.CreatedAtRoute(
                routeName: "GetDancerAsync",
                routeValues:
                new { dancerId = postDancerResponseDto.DancerId }
               );
        }

        public static async Task<Results<NotFound, Ok<RequestDancerDto>>> GetDancerAsync(SimoneDbContext dbContext,
            IMapper mapper, Guid dancerId)
        {
            var datamodel = await dbContext.DancerDataModels
                .FirstOrDefaultAsync(x => x.DancerId == dancerId);
            var dancer = mapper.Map<Dancer>(datamodel);
            return (dancer != null)
            ? TypedResults.Ok(mapper.Map<RequestDancerDto>(dancer))
            : TypedResults.NotFound();

        }

        public static async Task<Results<NotFound, NoContent>> PutDancer(SimoneDbContext dbContext,
            IMapper mapper, Guid dancerId, UpdateDancerDto updateDancerDto)
        {
            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d =>
            d.DancerId == dancerId);

            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(updateDancerDto, dancerDataModel);
            await dbContext.SaveChangesAsync();

            return TypedResults.NoContent();

        }

        public static async Task<Results<NotFound, NoContent>> DeleteDancer(SimoneDbContext dbContext,
        IMapper mapper, Guid dancerId)

        {
            var dancerDataModel = await dbContext.DancerDataModels
            .FirstOrDefaultAsync(d => d.DancerId == dancerId);

            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            dbContext.DancerDataModels.Remove(dancerDataModel);
            await dbContext.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    }
}
