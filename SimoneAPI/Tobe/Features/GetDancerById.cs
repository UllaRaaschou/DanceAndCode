using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SimoneAPI.Tobe.Features
{

    public static class GetDancerById
    {
        public static void RegisterDancersEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            endpointRouiteBuilder.MapGet("dancers/{dancerId:guid}", Get);
        }

        public static async Task<Results<NotFound, Ok<GetDancerResponceDto>>> Get(SimoneDbContext dbContext, IMapper mapper, Guid dancerId)
        {
            var datamodel = await dbContext.DancerDataModels.FirstOrDefaultAsync(x => x.DancerId == dancerId);
            var responceDto = mapper.Map<GetDancerResponceDto>(datamodel);

            return responceDto != null ? TypedResults.Ok(responceDto) : TypedResults.NotFound();
        }

    }

    public class GetDancerResponceDto
    {
        public Guid DancerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<String> Teams { get; set; } = new List<String>();

    }
}
