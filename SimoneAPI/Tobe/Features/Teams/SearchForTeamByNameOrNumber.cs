using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using System.Collections.ObjectModel;

namespace SimoneAPI.Tobe.Features
{
    public static class SearchForTeamByNameOrNumber

    {
        public static async Task<IResult> Get(SimoneDbContext dbContext,
            [FromQuery] String? name, [FromQuery] int? number)
        {
            var models = dbContext.TeamDataModels.Include(t => t.TeamDancerRelations).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(t => t.Name.Contains(name));
            }

            if (number.HasValue)
            {
                models = models.Where(t => t.Number == number);
            }

            List<TeamDataModel> res = await models.ToListAsync();

            if (res.Count == 0)
            {
                return TypedResults.NotFound();
            }

            Collection<TeamDto> teamDtos = new Collection<TeamDto>();
            foreach (var teamDataModel in res)
            {
                var teamDto = new TeamDto();
                teamDto.TeamId = teamDataModel.TeamId;
                teamDto.Name = teamDataModel.Name;
                teamDto.Number = teamDataModel.Number.ToString();
                teamDto.SceduledTime = teamDataModel.ScheduledTime.ToString();
                var dancerIdsOnTeam = new List<Guid>();
                var dancersOnTeam = new List<DancerDto>();
                if(teamDataModel.TeamDancerRelations!= null) 
                {                    
                    dancerIdsOnTeam.AddRange(teamDataModel.TeamDancerRelations.Select(tdr => tdr.DancerId));
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

    public class TeamDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;     
        public string SceduledTime { get; set; } = string.Empty;
        public List<DancerDto> DancersOnTeam { get; set; } = [];
    }

    public class DancerDto
    {
        public Guid DancerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly TimeOfBirth { get; set; }
        public bool IsTrialLesson { get; set; } = false;
        public DateOnly LastDanceDate { get; set; } = DateOnly.MinValue;

    }


}
