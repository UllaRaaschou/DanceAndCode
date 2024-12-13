using SimoneAPI.Tobe.Features;
using SimoneAPI.Tobe.Features.Attendances;
using SimoneAPI.Tobe.Features.Dancer;
using SimoneAPI.Tobe.Features.Dancers;
using SimoneAPI.Tobe.Features.StaffMembers;
using SimoneAPI.Tobe.Features.Teams;
using SimoneAPI.Tobe.Features.WorkRegistration;

namespace SimoneAPI.EndpointExtensions
{
    public static class Extensions
    {
        public static void RegisterDancersEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var dancersEndpoints = endpointRouiteBuilder.MapGroup("/dancers").WithTags("Dancers");
            dancersEndpoints.MapGet("", GetAllDancersOnTeam.Get)
                .WithName("GetAllDancersOnTeam")
                .WithOpenApi()
                .WithSummary("Get all dancers on a team");
            
            //.RequireAuthorization();
            var dancersWithGuidEndpoints = dancersEndpoints.MapGroup("/{dancerId:guid}");

            dancersWithGuidEndpoints.MapGet("", GetDancerById.Get)
                .WithName("GetDancer")
                .WithOpenApi()
                .WithSummary("Get a dancer by adding an id")
                .WithDescription("This could be a longer desccription of the process");            

            dancersEndpoints.MapPost("", PostDancer.Post)
                .WithName("PostDancer")
                .WithOpenApi()
                .WithSummary("Post a dancer");

            dancersEndpoints.MapPut("/{dancerId:guid}/Teams/{teamId}", AddTeamToDancersListOfTeams.AddItemToListOfTeams)
                .WithName("AddTeamToDancersTeamList")
                .WithOpenApi()
                .WithSummary("Add a team to a dancers list of teams");

            dancersWithGuidEndpoints.MapDelete("", DeleteDancer.Delete)
                .WithName("DeleteDancer")
                .WithOpenApi()
                .WithSummary("Delete a dancer from database");

            dancersEndpoints.MapGet("/SearchDancerFromNameOnly", SearchDancerFromNameOnly.Search)
                .WithName("SearchForDancerFromName")
                .WithOpenApi()
                .WithSummary("Write a name and search for dancers from this property");

            dancersEndpoints.MapGet("/Search", SearchForDancerByName.SearchForDancer)
                .WithName("SearchForDancer")
                .WithOpenApi()
                .WithSummary("Write a name and search for dancers with that name");

            dancersEndpoints.MapGet("/Check", TjeckForDancerRegisteredStatus.CheckForNOTRegistered)
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

            teamsEndpoints.MapGet("/all", GetAllTeams.Get)
                .WithName("Get all teams")
                .WithOpenApi()
                .WithSummary("Returns a list of all teams");

            teamsWithGuidEndpoints.MapGet("/danceDates", GetTeamDancedates.Get)
                .WithName("Get the dancedates of the team")
                .WithOpenApi()
                .WithSummary("Returns a list of dancedates for the team");


            teamsEndpoints.MapPost("", PostTeam.Post)
                .WithName("PostTeam")
                .WithOpenApi()
                .WithSummary("Post a new Team");

            teamsEndpoints.MapPut("", UpdateTeam.Put)
                .WithName("UpdateTeam")
                .WithOpenApi()
                .WithSummary("Update basic data of a team");
         
            teamsWithGuidEndpoints.MapGet("", GetTeamById.Get)
                .WithName("GetTeamById")
                .WithOpenApi()
                .WithSummary("Get a team from it's id");

            teamsWithGuidEndpoints.MapDelete("", DeleteTeam.Delete)
                .WithName("DeleteTeam")
                .WithOpenApi()
                .WithSummary("Delete a team using it's id");

            teamsWithGuidEndpoints.MapPut("/AddToListOfDancers", AddDancerToTeam.Put)
                .WithName("Add Dancer to Team")
                .WithOpenApi()
                .WithSummary("Add a dancer to teams list of dancers");

            teamsWithGuidEndpoints.MapDelete("/DeleteFromListOfDancers", DeleteDancerFromTeam.Delete)
               .WithName("Delete Dancer From a Team")
               .WithOpenApi()
               .WithSummary("Delete a dancer from a teams list of dancers");

            teamsWithGuidEndpoints.MapPut("/addTrialLesson", AddDancerToTrialLessonOnTeam.Put)
                .WithName("AddDancerToTrialLessonOnATeam")
                .WithOpenApi()
                .WithSummary("Add a dancer to a trial lesson on a team");

        }


        public static void RegisterRelationEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var relationEndpoints = endpointRouiteBuilder.MapGroup("/Relations").WithTags("Relations");

            relationEndpoints.MapGet("/{teamId:guid}", GetAllTeamDancerRelations.Get)
                .WithName("GetAllTeamDancerRelations")
                .WithOpenApi()
                .WithSummary("Get all teamDancer relations for a specific team");

            relationEndpoints.MapPut("/{teamId:guid}", UpdateAttendancesWithinRelations.SaveAttendances)
                .WithName("UpdateAttendancesWithinRelations")
                .WithOpenApi()
                .WithSummary("Save all the attendances on a specific team");



        }

        public static void RegisterAttendanceEndpoints(this IEndpointRouteBuilder endpointRouiteBuilder)
        {
            var attendanceEndpoints = endpointRouiteBuilder.MapGroup("/Attendances").WithTags("Attendance");

            attendanceEndpoints.MapPost("", PostAttendance.Post)
                .WithName("PostAttendance")
                .WithOpenApi()
                .WithSummary("Post a new attendance for a dancer on a team");

            attendanceEndpoints.MapGet("/Attendances/{teamId}/{dancerId}/{date}\"", GetAttendances.Get)
                .WithName("GetAttendances")
                .WithOpenApi()
                .WithSummary("Get a spcific attendances");

            attendanceEndpoints.MapDelete("", DeleteAttendance.Delete)
               .WithName("DeleteAttendance")
               .WithOpenApi()
               .WithSummary("Delete a dancers attendance on a team");
        }

        public static void RegisterStaffEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var staffEndpoints = endpointRouteBuilder.MapGroup("/StaffMember")
                .WithTags("StaffMember")
                .WithOpenApi();

            var staffWithGuidEndpoints = staffEndpoints.MapGroup("/{staffId:guid}");

            staffEndpoints.MapPost("", PostStaff.Post)
                .WithSummary("Post a new member of staff");

            staffEndpoints.MapGet("/SearchForStaffByName", SearchForStaffByName.Get)
                .WithName("SearchForStaffFromName")
                .WithOpenApi()
                .WithSummary("Write a name and search for staff from this property");

            staffWithGuidEndpoints.MapGet("", GetStaffById.Get)
                .WithSummary("Get a member of staff by Id");

            staffWithGuidEndpoints.MapPut("", UpdateStaff.Put)
                .WithName("UpdateStaff")
                .WithSummary("Update basic data of a member of staff by Id");

            staffWithGuidEndpoints.MapDelete("", DeleteStaff.Delete)
                .WithName("DeleteStaff")
                .WithOpenApi()
                .WithSummary("Delete a member of staff");
        }


        public static void RegisterWorkingHoursEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var workingHoursEndpoints = endpointRouteBuilder.MapGroup("/WorkingHours")
                .WithTags("WorkingHours")
                .WithOpenApi();

            workingHoursEndpoints.MapGet("/{staffId:guid}", GetWorkingHours.Get)
                .WithSummary("Post a new registration of working hours");

            workingHoursEndpoints.MapPost("", RegisterWorkingHours.Register)
                .WithSummary("Post a new registration of working hours");
            
        }
    }
}
