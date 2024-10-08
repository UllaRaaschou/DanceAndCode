using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DbContexts;
using System.Collections.ObjectModel;

namespace SimoneAPI.Tobe.Features.Dancers
{
    public class SearchDancerFromNameOrTimeOfBirth
    {
        public async static Task<IResult> Search(SimoneDbContext dbContext, IMapper mapper,
            [FromQuery(Name = "name")] string? name,
            [FromQuery(Name = "timeOfBirth")] DateOnly? timeOfBirth)
        {
            var dancerModels = await dbContext.DancerDataModels
                .Include(d => d.TeamDancerRelations)
                .ThenInclude(tdr => tdr.TeamDataModel)
                .Where(d =>
                   (name != null && d.Name.Contains(name)) ||
                   (timeOfBirth != null && d.TimeOfBirth == timeOfBirth))
                .ToListAsync();

            if (dancerModels == null)
            {
                return TypedResults.NotFound();
            }

            var dancerDtos = dancerModels
                .Select(dm => mapper.Map<ResponceDto>(dm))
                .ToList();
            return TypedResults.Ok(dancerDtos);
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
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public string TeamDetails { get; set; } = string.Empty;

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