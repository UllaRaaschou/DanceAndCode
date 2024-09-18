using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class UpdateTeam
    {
        public static void RegisterTeamsEndpoint(this WebApplication endPointRouteBuilder)
        {
            endPointRouteBuilder.MapPost("", Put);
        }

        public static async Task<IResult> Put(SimoneDbContext dbContext, IMapper mapper, UpdateTeamDto dto) 
        {
            var teamToUpdate = await dbContext.TeamDataModels.FirstOrDefaultAsync(tdr => tdr.TeamId == dto.TeamId);
            if (teamToUpdate == null)
            {
                return TypedResults.NotFound();
            }
            var updatedTeam = mapper.Map<TeamDataModel>(dto);
            await dbContext.SaveChangesAsync();

            var responceDto = mapper.Map<UpdateTeamResponceDto>(updatedTeam);
            return TypedResults.Ok(responceDto);
        }

        public class UpdateTeamDto 
        
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
        }

        public class UpdateTeamResponceDto

        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
        }
    }
}
