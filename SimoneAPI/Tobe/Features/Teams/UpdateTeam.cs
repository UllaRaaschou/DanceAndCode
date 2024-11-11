using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using SimoneAPI.Entities;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class UpdateTeam
    {
        public static void RegisterTeamsEndpoint(this WebApplication endPointRouteBuilder)
        {
            endPointRouteBuilder.MapPost("", Put);
        }

        public static async Task<IResult> Put(SimoneDbContext dbContext, [FromBody] UpdateTeamDto dto) 
        {
            var teamToUpdate = await dbContext.TeamDataModels.FirstOrDefaultAsync(tdr => tdr.TeamId == dto.TeamId);
            if (teamToUpdate == null)
            {
                return TypedResults.NotFound();
            }
            teamToUpdate.TeamId = dto.TeamId;
            teamToUpdate.Number = dto.Number;
            teamToUpdate.Name = dto.Name;
            teamToUpdate.ScheduledTime = dto.ScheduledTime;
            teamToUpdate.DayOfWeek = dto.DayOfWeek;
            try
            {


                await dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            var updatedTeam = new UpdateTeamResponceDto
            {
                TeamId = teamToUpdate.TeamId,
                Number = teamToUpdate.Number.ToString(),
                Name = teamToUpdate.Name,
                SceduledTime = teamToUpdate.ScheduledTime,
                Dancers = teamToUpdate.TeamDancerRelations.Select(tdr =>
                new DancerDto()
                {
                    DancerId = tdr.DancerId,
                    Name = tdr.DancerDataModel.Name,
                    TimeOfBirth = tdr.DancerDataModel.TimeOfBirth

                }).ToList()
            };          
            
            return TypedResults.Ok(updatedTeam);
        }

        public class UpdateTeamDto 
        
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public DayOfWeek DayOfWeek { get; set; } = default;
            public string ScheduledTime { get; set; } = string.Empty;
            public List<DancerDto> DancersOnTeam { get; set; } = new List<DancerDto>();
        }

        public class UpdateTeamResponceDto

        {
            public Guid TeamId { get; set; }
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public List<DancerDto> Dancers = new List<DancerDto>();
        }
    }
}
