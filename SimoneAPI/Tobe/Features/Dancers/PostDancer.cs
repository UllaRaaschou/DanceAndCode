using AutoMapper;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class PostDancer
    {
        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/Dancers", Post);

        }

        public static async Task<IResult> Post(SimoneDbContext dbContext, IMapper mapper, PostDancerDto dto)
        {
            var dataModel = mapper.Map<DancerDataModel>(dto);
            dbContext.DancerDataModels.Add(dataModel);
            await dbContext.SaveChangesAsync();

            var postDancerResponseDto = mapper.Map<PostDancerResponseDto>(dataModel);

            return TypedResults.Created("/dancers", postDancerResponseDto);

            //return TypedResults.CreatedAtRoute(
            //    routeName: "GetDancerAsync",
            //    routeValues:
            //    new { dancerId = postDancerResponseDto.DancerId }
            //   );
        }

        public class PostDancerDto
        {
            public string Name { get; set; } = string.Empty;
            [DataType(DataType.Date)]
            public DateTime TimeOfBirth { get; set; }
        }

        public class PostDancerResponseDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; }
            public DateTime TimeOfBirth { get; set; }
        }
    }
}
