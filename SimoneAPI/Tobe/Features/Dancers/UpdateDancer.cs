using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class UpdateDancer
    {
        public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("/Dancers", Put);

        }

       
        public static async Task<Results<NotFound, Ok<UpdateDancerResponceDto>>> Put(SimoneDbContext dbContext,
            IMapper mapper, Guid dancerId, UpdateDancerDto updateDancerDto)
        {
            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == dancerId);

            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(updateDancerDto, dancerDataModel);

            await dbContext.SaveChangesAsync();

            var responseDto = mapper.Map<UpdateDancerResponceDto>(dancerDataModel);

            return TypedResults.Ok(responseDto);
        }

        public class UpdateDancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateOnly TimeOfBirth { get; set; }
        }

        public class UpdateDancerResponceDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateOnly TimeOfBirth { get; set; }
            //public List<string> Teams { get; set; } = new List<string>();
        }

     

        public class TeamDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public ICollection<RequestDancerDto>? EnrolledDancers { get; set; } = new List<RequestDancerDto>();
        }


    }
}
