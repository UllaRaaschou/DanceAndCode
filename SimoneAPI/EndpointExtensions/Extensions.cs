using SimoneAPI.EndpointHandlers;
using SimoneAPI.Tobe.Features;
using System.Runtime.CompilerServices;

namespace SimoneAPI.EndpointExtensions
{
    public static class Extensions
    {
        public static void RegisterDancersEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var dancersEndpoints = endpointRouiteBuilder.MapGroup("/dancers");
                //.RequireAuthorization();
            var dancersWithGuidEndpoints = dancersEndpoints.MapGroup("/{dancerId:guid}");

            dancersWithGuidEndpoints.MapGet("", DancersHandlers.GetDancerByIdAsync)
                .WithName("GetDancerAsync")
                .WithOpenApi()
                .WithSummary("Get a dancer by adding an id")
                .WithDescription("This could be a longer desccription of the process");
            dancersEndpoints.MapGet("", DancersHandlers.GetDancersOnTeamAsync);
            dancersEndpoints.MapPost("", PostDancer.Post);
            //dancersWithGuidEndpoints.MapPut("", DancersHandlers.PutDancer);
                //.AddEndpointFilter(async(dbContext, next) =>
                //{ 
                //    var result = await next.Invoke(dbContext);
                //};
            dancersWithGuidEndpoints.MapDelete("", DancersHandlers.DeleteDancer);
        }

        public static void RegisterTeamsEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var teamsEndpoints = endpointRouiteBuilder.MapGroup("/teams");
                //.RequireAuthorization();
            var teamsWithGuidEndpoints = teamsEndpoints.MapGroup("/{teamId:guid}");

            teamsEndpoints.MapPost("", () =>
            {
                throw new NotImplementedException();
            });
            //teamsEndpoints.MapPost("", TeamsHandlers.PostTeamAsync);
            teamsWithGuidEndpoints.MapGet("", TeamsHandlers.GetTeamAsync).WithName("GetTeamAsync");
            teamsEndpoints.MapGet("", TeamsHandlers.GetTeamsAsync);
                            

        }
    }
}
