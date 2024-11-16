using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using System.Collections.ObjectModel;

namespace SimoneAPI.Tobe.Features.Teams
{
    public class GetAllTeams
    {
        
            public static async Task<IResult> Get(SimoneDbContext dbContext)
            {
                var teams = await dbContext.TeamDataModels
                            .Include(t => t.TeamDancerRelations)
                            .ThenInclude(tdr => tdr.DancerDataModel)
                            .ToListAsync();

            Collection<TeamDto> teamDtos = new Collection<TeamDto>();
            foreach (var teamDataModel in teams)
            {
                var teamDto = new TeamDto();
                teamDto.TeamId = teamDataModel.TeamId;
                teamDto.Name = teamDataModel.Name;
                teamDto.Number = teamDataModel.Number.ToString();
                teamDto.SceduledTime = teamDataModel.ScheduledTime.ToString();
                var dancerIdsOnTeam = new List<Guid>();
                var dancersOnTeam = new List<DancerDto>();
                if (teamDataModel.TeamDancerRelations != null)
                {
                    dancerIdsOnTeam.AddRange(teamDataModel.TeamDancerRelations.Select(tdr => tdr.DancerId));
                    dancersOnTeam.AddRange(await dbContext.DancerDataModels
                        .Where(ddm => dancerIdsOnTeam.Contains(ddm.DancerId))
                        .Select(ddm => new DancerDto { DancerId = ddm.DancerId, Name = ddm.Name, TimeOfBirth = ddm.TimeOfBirth, LastDanceDate = ddm.LastDanceDate }).ToListAsync());
                }
                teamDto.DancersOnTeam.AddRange(dancersOnTeam);
                teamDtos.Add(teamDto);
            }
            return TypedResults.Ok(teamDtos);
        }
            
                

                            

                return teams != null
                ? TypedResults.Ok(mapper.Map<RequestTeamDto>(team))
                : TypedResults.NotFound();
            }

            
    }
}
