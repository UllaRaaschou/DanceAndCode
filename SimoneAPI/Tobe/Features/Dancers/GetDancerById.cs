using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SimoneAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace SimoneAPI.Tobe.Features.Dancer
{

    public static class GetDancerById
    {
        public static async Task<Results<NotFound, Ok<GetDancerResponceDto>>> Get(SimoneDbContext dbContext, IMapper mapper, Guid dancerId)
        {
            var datamodel = await dbContext.DancerDataModels.FirstOrDefaultAsync(x =>
            x.DancerId == dancerId);
            var responceDto = mapper.Map<GetDancerResponceDto>(datamodel);

            return responceDto != null 
                ? TypedResults.Ok(responceDto) 
                : TypedResults.NotFound();
        }
    }









    public class GetDancerResponceDto
    {
        public Guid DancerId { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateOnly TimeOfBirth { get; set; }
        public IEnumerable<GetTeamResponceDto> Teams { get; set; } = new List<GetTeamResponceDto>();

    }

    public class GetTeamResponceDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; } = 0;
        public IEnumerable<GetDancerResponceDto> DancersOnTeam { get; set; } = Enumerable.Empty<GetDancerResponceDto>();
    }
}
