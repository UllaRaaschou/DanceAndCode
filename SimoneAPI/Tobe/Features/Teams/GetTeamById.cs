using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using System.Collections.Generic;

namespace SimoneAPI.Tobe.Features.Teams
{
    public static class GetTeamById
    {
        public static async Task<Results<NotFound, Ok<RequestTeamDto>>> Get(SimoneDbContext dbContext,Guid teamId)
        {
            var team = await dbContext.TeamDataModels
                .Include(rel => rel.TeamDancerRelations)
                .ThenInclude(d => d.DancerDataModel)
                .FirstOrDefaultAsync(x => x.TeamId == teamId);

            //List<DancerDataModel> dancerModelsOnTeam = team?.TeamDancerRelations.Select(tdr => tdr.DancerDataModel).ToList() ?? new List<DancerDataModel>();

            var teamDto = new RequestTeamDto()
            {
                TeamId = teamId,
                Number = team.Number,
                Name = team.Name,
                ScheduledTime = team.ScheduledTime,
                DancersOnTeam = team.TeamDancerRelations.Select(rel => new RequestDancerDto()
                {
                    DancerId = rel.DancerDataModel.DancerId,
                    Name = rel.DancerDataModel.Name

                }).ToList()

            };

            return teamDto != null
            ? TypedResults.Ok(teamDto)
            : TypedResults.NotFound();
        }

        public class RequestTeamDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string ScheduledTime { get; set; } = string.Empty;
            public List<RequestDancerDto> DancersOnTeam { get; set; } = new List<RequestDancerDto>();

        }

        //public class RequestDancerDto
        //{
        //    public Guid DancerId { get; set; }
        //    public string Name { get; set; } = string.Empty;

        //}
    }
}
