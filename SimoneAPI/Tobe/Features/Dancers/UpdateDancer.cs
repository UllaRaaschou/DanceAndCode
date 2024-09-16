using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SimoneAPI.DataModels;
using SimoneAPI.DbContexts;
using SimoneAPI.Dtos.Dancer;
using SimoneAPI.Dtos.Team;

namespace SimoneAPI.Tobe.Features.Dancer
{
    public static class UpdateDancer
    {
        public static void RegisterDancerEndponts(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPut("/Dancers", Put);

        }

        //public static async Task<Results<NotFound, IEnumerable<SearchDancerResponceDto>>> SearchForDancer(SimoneDbContext dbContext,


        public static async Task<Results<NotFound, Ok<UpdateDancerResponceDto>>> Put(SimoneDbContext dbContext,
            IMapper mapper, Guid dancerId, UpdateDancerDto updateDancerDto)
        {
            var dancerDataModel = await dbContext.DancerDataModels.FirstOrDefaultAsync(d => d.DancerId == dancerId);

            if (dancerDataModel == null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(updateDancerDto, dancerDataModel);

            if (dancerDataModel.TeamDancerRelations != null)
            {
                dancerDataModel.TeamDancerRelations.Clear();

                foreach (var teamName in updateDancerDto.Teams ?? Enumerable.Empty<string>())
                {
                    var teamDataModel = await dbContext.TeamDataModels.FirstOrDefaultAsync(t =>
                    t.Name == teamName);

                    if (teamDataModel != null)
                    {
                        dancerDataModel.TeamDancerRelations.Add(new TeamDancerRelation
                        {
                            TeamId = teamDataModel.TeamId,
                            DancerId = dancerDataModel.DancerId
                        });
                    }
                }
            }

            await dbContext.SaveChangesAsync();

            var responseDto = mapper.Map<UpdateDancerResponceDto>(dancerDataModel);

            if (dancerDataModel.TeamDancerRelations != null)
            {
                foreach (var relation in dancerDataModel.TeamDancerRelations)
                {
                    var teamDataModel = await dbContext.TeamDataModels.FirstOrDefaultAsync(t => t.TeamId == relation.TeamId);
                    var teamName = teamDataModel?.Name;
                    if (teamName != null)
                    {
                        responseDto.Teams.Add(teamName);
                    }
                }
            }

            return TypedResults.Ok(responseDto);
        }

        public class UpdateDancerDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public IEnumerable<string>? Teams { get; set; } = null;
        }

        public class UpdateDancerResponceDto
        {
            public Guid DancerId { get; set; }
            public string Name { get; set; } = string.Empty;
            public DateTime TimeOfBirth { get; set; }
            public List<string> Teams { get; set; } = new List<string>();
        }

        //public class DancerDataModel
        //{
        //    public Guid DancerId { get; set; }
        //    public string Name { get; set; } = string.Empty;
        //    public DateTime TimeOfBirth { get; set; }
        //    public ICollection<TeamDancerRelation>? TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();
        //}

        //public class TeamDancerRelation
        //{
        //    public Guid TeamDancerRelationId { get; set; }
        //    public Guid TeamId { get; set; }
        //    public Guid DancerId { get; set; }
        //    public UpdateDancer.TeamDataModel TeamDataModel { get; set; } = null!;
        //    public DancerDataModel DancerDataModel { get; set; } = null!;
        //}

        //public class TeamDataModel
        //{
        //    public Guid? TeamId { get; set; }
        //    public int Number { get; set; } = 0;
        //    public string Name { get; set; } = string.Empty;
        //    public string SceduledTime { get; set; } = string.Empty;

        //    public ICollection<TeamDancerRelation> TeamDancerRelations { get; set; } = new HashSet<TeamDancerRelation>();
        //}

        public class TeamDto
        {
            public Guid TeamId { get; set; }
            public int Number { get; set; } = 0;
            public string Name { get; set; } = string.Empty;
            public string SceduledTime { get; set; } = string.Empty;
            public ICollection<RequestDancerDto>? EnrolledDancers { get; set; } = new List<RequestDancerDto>();
        }


    }
}
