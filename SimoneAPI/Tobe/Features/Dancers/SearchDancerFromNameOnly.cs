using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public class SearchDancerFromNameOnly
    {
        public async static Task<IResult> Search(SimoneDbContext dbContext, IMapper mapper,
            [FromQuery(Name = "name")] string? name)
        {
            var dancerModels = await dbContext.DancerDataModels
                .Include(d => d.TeamDancerRelations)
                .ThenInclude(tdr => tdr.TeamDataModel)
                .Where(d => name != null && d.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            if (dancerModels == null)
            {
                return TypedResults.NotFound();
            }

            var returnDtos = new List<ResponceDto>();
            returnDtos.AddRange(dancerModels.Select(d => new ResponceDto
            {
                DancerId = d.DancerId,
                Name = d.Name,
                TimeOfBirth = d.TimeOfBirth.ToString(),
                Teams = new Collection<TeamDto>(d.TeamDancerRelations
                        .Select(tdr =>
                        new TeamDto
                        {
                            TeamId = tdr.TeamDataModel.TeamId,
                            Number = tdr.TeamDataModel.Number.ToString(),
                            Name = tdr.TeamDataModel.Name,
                            SceduledTime = tdr.TeamDataModel.ScheduledTime,
                            IsTrialLesson = tdr.IsTrialLesson
                        }
                        ).ToList())
            }));
            

            return TypedResults.Ok(returnDtos);
        }

        public class ResponceDto
        { 
        
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string TimeOfBirth { get; set; } = string.Empty;
            public Collection<TeamDto> Teams { get; set; } = new Collection<TeamDto>();
            //public Collection<string> TeamsAndDetails { get; set; } = new Collection<string>();          
        }

        public class TeamDto
        {
            public Guid TeamId { get; set; }
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public string TeamDetails { get; set; } = string.Empty;
            public bool IsTrialLesson { get; set; } = false;

            public TeamDto()
            {
                UpdateTeamDetails();
            }

            private void UpdateTeamDetails()
            {
                TeamDetails = $"Hold {Number} '{Name}' - {SceduledTime}";
            }

        }
    }

}    