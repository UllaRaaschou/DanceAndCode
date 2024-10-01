using Microsoft.AspNetCore.Builder;
using SimoneAPI.EndpointHandlers;
using SimoneAPI.Tobe.Features;
using SimoneAPI.Tobe.Features.Attendances;
using SimoneAPI.Tobe.Features.Dancer;
using SimoneAPI.Tobe.Features.Dancers;
using SimoneAPI.Tobe.Features.StaffMembers;
using SimoneAPI.Tobe.Features.Teams;
using System.Runtime.CompilerServices;

namespace SimoneAPI.EndpointExtensions
{
    public static class Extensions
    {
        public static void RegisterDancersEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var dancersEndpoints = endpointRouiteBuilder.MapGroup("/dancers").WithTags("Dancers");
                //.RequireAuthorization();
            var dancersWithGuidEndpoints = dancersEndpoints.MapGroup("/{dancerId:guid}");

            dancersWithGuidEndpoints.MapGet("", GetDancerById.Get)
                .WithName("GetDancer")
                .WithOpenApi()
                .WithSummary("Get a dancer by adding an id")
                .WithDescription("This could be a longer desccription of the process");

            dancersEndpoints.MapGet("", GetAllDancersOnTeam.Get)
                .WithName("GetAllDancersOnTeam")
                .WithOpenApi()
                .WithSummary("Get all dancers on a team");

            dancersEndpoints.MapPost("", PostDancer.Post)
                .WithName("PostDancer")
                .WithOpenApi()
                .WithSummary("Post a dancer");

            dancersEndpoints.MapPut("", AddTeamToDancersListOfTeams.AddItemToListOfTeams)
                .WithName("AddTeamToDancersTeamList")
                .WithOpenApi()
                .WithSummary("Add a team to a dancers list of teams");

            dancersWithGuidEndpoints.MapDelete("", DeleteDancer.Delete)
                .WithName("DeleteDancer")
                .WithOpenApi()
                .WithSummary("Delete a dancer from database");

            //dancersWithGuidEndpoints.MapDelete("/Teams/{TeamId}", DeleteItemFromDancersListOfTeams.DeleteItemFromListOfTeams)
            //    .WithName("DeleteItemFromDancersListOfTeams")
            //    .WithOpenApi()
            //    .WithSummary("Delete a team from a dancers list of teams");

            dancersEndpoints.MapGet("/SerachForDancerFromNameOrBirthday/{Name}/{timeOfBirth}", SearchDancerFromNameOrTimeOfBirth.Search)
                .WithName("SearchForDancerFromNameOrBirthday")
                .WithOpenApi()
                .WithSummary("Write a name or a time of birth and search for dancers from one of these properties");

            dancersEndpoints.MapGet("/Search", SearchForDancerByName.SearchForDancer)
                .WithName("SearchForDancer")
                .WithOpenApi()
                .WithSummary("Write a name and search for dancers with that name");

            dancersEndpoints.MapGet("/Check", GetDancerByNameAndTimeOfBirth.CheckForNOTRegistered)
                .WithName("CheckForNOTRegistered")
                .WithOpenApi()
                .WithSummary("Write a name and time of birth and get a false returned, if danser is already registered");

            dancersWithGuidEndpoints.MapPut("", UpdateDancer.Put)
                .WithName("UpdateDancer")
                .WithOpenApi()
                .WithSummary("Update basic data of a dancer");

            dancersWithGuidEndpoints.MapDelete("/Teams/{teamId}", DeleteTeamFromDancersListOfTeams.Delete)
                .WithName("DeleteTeamFromDancersListOfTeams")
                .WithOpenApi()
                .WithSummary("Delete a team from a dancers list of teams");
        }

        public static void RegisterTeamsEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var teamsEndpoints = endpointRouiteBuilder.MapGroup("/Teams").WithTags("Teams");
                //.RequireAuthorization();
            var teamsWithGuidEndpoints = teamsEndpoints.MapGroup("/{teamId:guid}");


            teamsEndpoints.MapGet("", SearchForTeamByNameOrNumber.Get)
                .WithName("Search by team name or number")
                .WithOpenApi()
                .WithSummary("Write a team number or a team name/part of a name and the system will return a match if possible");


            teamsEndpoints.MapPost("", PostTeam.Post)
                .WithName("PostTeam")
                .WithOpenApi()
                .WithSummary("Post a new Team");

            teamsEndpoints.MapPut("", UpdateTeam.Put)
                .WithName("UpdateTeam")
                .WithOpenApi()
                .WithSummary("Update basic data of a team");

            //teamsEndpoints.MapGet("")

            teamsWithGuidEndpoints.MapGet("", GetTeamById.Get)
                .WithName("GetTeamById")
                .WithOpenApi()
                .WithSummary("Get a team from it's id"); 

            teamsWithGuidEndpoints.MapDelete("", DeleteTeam.Delete)
                .WithName("DeleteTeam")
                .WithOpenApi()
                .WithSummary("Delete a team using it's id"); 

            teamsWithGuidEndpoints.MapPut("/addToListOfDancers", AddDancerToTeam.Put)
                .WithName("Add Dancer to Team")
                .WithOpenApi()
                .WithSummary("Add a dancer to teams list of dancers");

            teamsWithGuidEndpoints.MapPut("/addTrialLesson", AddDancerToTrialLessonOnTeam.Put)
                .WithName("AddDancerToTrialLessonOnATeam")
                .WithOpenApi()
                .WithSummary("Add a dancer to a trial lesson on a team");

        }

        public static void RegisterAttendanceEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder) 
        {
            var attendanceEndpoints = endpointRouiteBuilder.MapGroup("/Attendance").WithTags("Attendance");

            attendanceEndpoints.MapPost("", PostAttendance.Post)
                .WithName("PostAttendance")
                .WithOpenApi()
                .WithSummary("Post a new attendance for a dancer on a team");

            attendanceEndpoints.MapGet("", GetAttendances.Get)
                .WithName("GetAttendances")
                .WithOpenApi()
                .WithSummary("Get a dancers attendances on a team");

            attendanceEndpoints.MapDelete("", DeleteAttendance.Delete)
               .WithName("DeleteAttendance")
               .WithOpenApi()
               .WithSummary("Delete a dancers attendance on a team");

            attendanceEndpoints.MapPut("", UpdateAttendance.Put)
              .WithName("UpdateAttendance")
              .WithOpenApi()
              .WithSummary("Update a dancers attendance on a team");
        }

        public static void RegisterStaffEndpoints(this IEndpointRouteBuilder endpointRouteBuilder) 
        {


            //TODO: BRUG TAG TIL AT GRUPPERE GET/PUT/POST SAMMEN SE STAFFMEMBER I SWAGGER
            var staffEndpoints = endpointRouteBuilder.MapGroup("/StaffMember")
                .WithTags("StaffMember")
                .WithOpenApi();

            var staffWithGuidEndpoints = staffEndpoints.MapGroup("staffEndpoints/{staffId:guid}");

            staffEndpoints.MapPost("", PostStaff.Post)
                .WithSummary("Post a new member of staff");

            staffWithGuidEndpoints.MapGet("", GetStaffById.Get)
                .WithSummary("Get a member of staff by Id");

            staffWithGuidEndpoints.MapPut("", UpdateStaff.Put)
                .WithName("UpdateStaff")
                .WithSummary("Update basic data of a member of staff by Id");

            staffWithGuidEndpoints.MapPut("/{workedHours:decimal}/{date:datetime}", RegisterWorkingHours.Register)
                .WithName("RegisterWorkingHours")
                .WithSummary("Add a new register of working hours");

            staffWithGuidEndpoints.MapGet("/{firstDayOfPeriod:dateOnly}/{lastDayInPeriod:dateOnly}", GetNumberOfWorkingHoursForPeriod.Get)
                .WithName("GeNumberOfWorkingHoursForPeriod")
                .WithSummary("Write a period and get the number of working hours for a staff in that periode returned");

            //staffEndpoints.MapPut("", UpdateStaff.Put)
            //    .WithName("UpdateStaff")
            //    .WithOpenApi()
            //    .WithSummary("Update a member of staff");

            //staffEndpoints.MapDelete("", DeleteStaff.Delete)
            //    .WithName("DeleteStaff")
            //    .WithOpenApi()
            //    .WithSummary("Delete a member of staff");


        }
    }
}
