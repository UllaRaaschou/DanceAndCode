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
                teamDto.ScheduledTime = teamDataModel.ScheduledTime.ToString();
                var dancerIdsOnTeam = new List<Guid>();
                var dancersOnTeam = new List<DancerDto>();

                if (teamDataModel.TeamDancerRelations != null)
                {
                    dancerIdsOnTeam.AddRange(teamDataModel.TeamDancerRelations
                        .Where(tdr => tdr.LastDanceDate >= DateOnly.FromDateTime(DateTime.Today))
                        .Select(tdr => tdr.DancerId));
                    dancersOnTeam.AddRange(await dbContext.DancerDataModels
                        .Where(ddm => dancerIdsOnTeam.Contains(ddm.DancerId))
                        .Select(ddm => new DancerDto { DancerId = ddm.DancerId, Name = ddm.Name, TimeOfBirth = ddm.TimeOfBirth }).ToListAsync());
                }
                teamDto.DancersOnTeam.AddRange(dancersOnTeam);
                teamDtos.Add(teamDto);
            }
            return TypedResults.Ok(teamDtos);
        }
    }           
   
}
