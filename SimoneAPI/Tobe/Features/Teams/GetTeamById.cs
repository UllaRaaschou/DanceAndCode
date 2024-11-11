using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class GetTeamById
    {
        public static void RegisterTeamsEndpoint(this WebApplication endPointRouteBuilder)
        {
            endPointRouteBuilder.MapGet("", Get);
        }

        public static async Task<Results<NotFound, Ok<RequestTeamDto>>> Get(SimoneDbContext dbContext,
        IMapper mapper, Guid teamId)
        {
            var team = await dbContext.TeamDataModels.FirstOrDefaultAsync(x => x.TeamId == teamId);

            return team != null
            ? TypedResults.Ok(mapper.Map<RequestTeamDto>(team))
            : TypedResults.NotFound();
        }

        public class RequestTeamDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public ICollection<RequestDancerDto> DancersOnTeam { get; set; } = new List<RequestDancerDto>();

        }

        //public class RequestDancerDto
        //{
        //    public Guid DancerId { get; set; }
        //    public string Name { get; set; } = string.Empty;

        //}
    }
}
