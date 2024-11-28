
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class UpdateDancer
    {
        public static void RegisterDancerEndpoint(this WebApplication endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("/Dancers", Put);

        }

       
        public static async Task<Results<NotFound, Ok<UpdateDancerResponceDto>>> Put(SimoneDbContext dbContext,
            Guid dancerId, UpdateDancerDto updateDancerDto)
        {
            var dancerDataModel = await dbContext.DancerDataModels
                .Include(x => x.TeamDancerRelations)
                .ThenInclude(x => x.TeamDataModel)
                .FirstOrDefaultAsync(d => d.DancerId == dancerId);

            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            dancerDataModel.Name = updateDancerDto.Name;
            dancerDataModel.TimeOfBirth = updateDancerDto.TimeOfBirth;
            //var relations = new List<TeamDancerRelation>();
            //updateDancerDto.Teams.Select(t => new TeamDancerRelation
            //{
            //    TeamId = t.TeamId,
            //    DancerId = updateDancerDto.DancerId,                
            //    IsTrialLesson = false,
            //    LastDanceDate = t.LastDancedate
            //});
                      

            await dbContext.SaveChangesAsync();

            var responseDto = new UpdateDancerResponceDto()
            {
                DancerId = dancerDataModel.DancerId,
                Name = dancerDataModel.Name,
                TimeOfBirth = dancerDataModel.TimeOfBirth,
                Teams = dancerDataModel.TeamDancerRelations.Select(tdr => new TeamDto
                    {
                        TeamId = tdr.TeamDataModel.TeamId,
                        Number = tdr.TeamDataModel.Number.ToString(),
                        Name = tdr.TeamDataModel.Name,
                        ScheduledTime = tdr.TeamDataModel.ScheduledTime,
                        DayOfWeek = tdr.TeamDataModel.DayOfWeek,
                        EnrolledDancers = tdr.TeamDataModel.TeamDancerRelations.Select(tdr =>
                        new RequestDancerDto
                        {
                            DancerId = tdr.DancerDataModel.DancerId,
                            Name = tdr.DancerDataModel.Name
                        }
                        ).ToList(),
                        LastDancedate = tdr.LastDanceDate                        
                    }).ToList()
            };

            

            return TypedResults.Ok(responseDto);
        }

        public class UpdateDancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateOnly TimeOfBirth { get; set; }
            public ObservableCollection<TeamDto> Teams { get; set; } = new ObservableCollection<TeamDto>();


        }

        public class UpdateDancerResponceDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateOnly TimeOfBirth { get; set; }
            public List<TeamDto> Teams { get; set; } = new List<TeamDto>();
            //public List<string> Teams { get; set; } = new List<string>();
        }

     

        public class TeamDto
        {
            public Guid TeamId { get; set; }
            public string Number { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string ScheduledTime { get; set; } = string.Empty;
            public ICollection<RequestDancerDto>? EnrolledDancers { get; set; } = new List<RequestDancerDto>();
            public DateOnly LastDancedate { get; set; } = default;
            public DayOfWeek DayOfWeek { get; set; } = default;
        }


    }
}
