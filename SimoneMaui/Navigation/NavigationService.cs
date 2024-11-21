
using SimoneMaui.Views;
using SimoneMaui.ViewModels.Dtos;
using SimoneMaui.Views.TeamViews;
namespace SimoneMaui.Navigation
{

    public class NavigationService : INavigationService
    {
        public const string FIRST_PAGE_ROUTE = "firstPage";

        public const string POST_DANCER_PAGE_ROUTE = "postDancer";
        public const string UPDATE_DANCER_PAGE_ROUTE = "updateDancer";
        public const string SEARCH_DANCER_PAGE_ROUTE = "searchDancer";
        public const string DELETE_DANCER_PAGE_ROUTE = "deleteDancer";


        public const string POST_TEAM_PAGE_ROUTE = "postTeam";
        public const string SEARCH_TEAM_PAGE_ROUTE = "searchTeam";
        public const string UPDATE_TEAM_PAGE_ROUTE = "updateTeam";
        public const string DELETE_TEAM_PAGE_ROUTE = "deleteTeam";
        public const string DELETE_DANCER_FROM_TEAM_PAGE_ROUTE = "deleteDancerFromTeam";


        public const string POST_STAFF_PAGE_ROUTE = "postStaff";
        public const string UPDATE_STAFF_PAGE_ROUTE = "updateStaff";
        public const string SEARCH_STAFF_PAGE_ROUTE = "searchStaff";
        public const string DELETE_STAFF_PAGE_ROUTE = "deleteStaff";

        public const string GET_WORKING_HOURS_PAGE_ROUTE = "getWorkingHours";




        public static void ConfigureRouter()
        {
            Routing.RegisterRoute(FIRST_PAGE_ROUTE, type: typeof(FirstPage));

            Routing.RegisterRoute(POST_DANCER_PAGE_ROUTE, type: typeof(PostDancerPage));
            Routing.RegisterRoute(UPDATE_DANCER_PAGE_ROUTE, type: typeof(UpdateDancerPage));
            Routing.RegisterRoute(SEARCH_DANCER_PAGE_ROUTE, type: typeof(SearchDancerPage));
            Routing.RegisterRoute(DELETE_DANCER_PAGE_ROUTE, type: typeof(DeleteDancerPage));
            Routing.RegisterRoute(DELETE_DANCER_FROM_TEAM_PAGE_ROUTE, type: typeof(DeleteDancerFromTeamPage));

            Routing.RegisterRoute(POST_TEAM_PAGE_ROUTE, type: typeof(PostTeamPage));
            Routing.RegisterRoute(SEARCH_TEAM_PAGE_ROUTE, type: typeof(SearchTeamPage));
            Routing.RegisterRoute(UPDATE_TEAM_PAGE_ROUTE, type: typeof(UpdateTeamPage));
            Routing.RegisterRoute(DELETE_TEAM_PAGE_ROUTE, type: typeof(DeleteTeamPage));

            Routing.RegisterRoute(POST_STAFF_PAGE_ROUTE, type: typeof(PostStaffPage));
            Routing.RegisterRoute(SEARCH_STAFF_PAGE_ROUTE, type: typeof(SearchStaffPage));
            Routing.RegisterRoute(UPDATE_STAFF_PAGE_ROUTE, type: typeof(UpdateStaffPage));
            Routing.RegisterRoute(DELETE_STAFF_PAGE_ROUTE, type: typeof(DeleteStaffPage));

            Routing.RegisterRoute(GET_WORKING_HOURS_PAGE_ROUTE, type: typeof(GetWorkingHoursPage)); 

        }

        public async Task GoToFirstPage()
        {
            await Shell.Current.GoToAsync($"{FIRST_PAGE_ROUTE}?unique={Guid.NewGuid()}");
        }



        public async Task GoToPostDancer()
        {
            await Shell.Current.GoToAsync(POST_DANCER_PAGE_ROUTE);
        }

        public async Task GoToUpdateDancer(DancerDto dancerDto)
        {
            var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto } };
            await Shell.Current.GoToAsync(UPDATE_DANCER_PAGE_ROUTE, parameters);
        }

        public async Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto)
        {
            var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto }, {"teamDto", teamDto } };
            await Shell.Current.GoToAsync(UPDATE_DANCER_PAGE_ROUTE, parameters);
        }

        public async Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto, bool wannaAddTeamToADancer, bool wannaAddTrialLessonToADancer)
        {
            var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto }, { "teamDto", teamDto },
                {"WannaAddTeamToADancer",  wannaAddTeamToADancer}, {"WannaAddTrialLessonToADancer", wannaAddTrialLessonToADancer } } ;
            await Shell.Current.GoToAsync(UPDATE_DANCER_PAGE_ROUTE, parameters);
        }

        public async Task GoToDeleteDancer(DancerDto dancerDto)
        {
            var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto } };
            await Shell.Current.GoToAsync(DELETE_DANCER_PAGE_ROUTE, parameters);
        }


        public async Task GoToSearchDancer()
        {
            await Shell.Current.GoToAsync(SEARCH_DANCER_PAGE_ROUTE);
        }
        public async Task GoToSearchDancer(TeamDto teamDto, bool puttingDancerOnTeam, bool addTrialDancerToTeam)
        {
            var parameters = new Dictionary<string, object> { { "teamDto", teamDto }, { "puttingDancerOnTeam", puttingDancerOnTeam }, { "addTrialDancerToTeam", addTrialDancerToTeam } };
            await Shell.Current.GoToAsync(SEARCH_DANCER_PAGE_ROUTE, parameters);
        }




        public async Task GoToPostTeam()
        {
            await Shell.Current.GoToAsync(POST_TEAM_PAGE_ROUTE);
        }

        public async Task GoToSearchTeam()
        {
            await Shell.Current.GoToAsync(SEARCH_TEAM_PAGE_ROUTE);
        }
        public async Task GoToSearchTeam(DancerDto dancerDto)
        {
            var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto } };
            await Shell.Current.GoToAsync(SEARCH_TEAM_PAGE_ROUTE, parameters);
        }
        public async Task GoToSearchTeam(DancerDto dancerDto, bool wannaAddTeamToADancer, bool wannaAddTrialLesson)
        {
            var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto }, { "WannaAddTeamToADancer", wannaAddTeamToADancer },
                {"WannaAddTrialLessonToADancer", wannaAddTrialLesson } };
            await Shell.Current.GoToAsync(SEARCH_TEAM_PAGE_ROUTE, parameters);
        }

        //public async Task GoToSearchTeam(DancerDto dancerDto, bool wannaAddTrialLessonToADancer)
        //{
        //    var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto }, { "WannaAddTrialLessonToADancer", wannaAddTrialLessonToADancer } };
        //    await Shell.Current.GoToAsync(SEARCH_TEAM_PAGE_ROUTE, parameters);
        //}

        public Task GoToUpdateTeam(TeamDto selectedTeam)
        {
            var parameters = new Dictionary<string, object> { { "teamDto", selectedTeam } };
            return Shell.Current.GoToAsync(UPDATE_TEAM_PAGE_ROUTE, parameters);
        }

        


        public Task GoToUpdateTeam(TeamDto selectedTeam, DancerDto selectedDancer, bool puttingDancerOnTeam, bool addTrialDancerToTeam)
        {
            var parameters = new Dictionary<string, object> { { "teamDto", selectedTeam }, { "dancerDto", selectedDancer }, { "puttingDancerOnTeam", puttingDancerOnTeam }, { "addTrialDancerToTeam", addTrialDancerToTeam } };
            return Shell.Current.GoToAsync(UPDATE_TEAM_PAGE_ROUTE, parameters);
        }

        public Task GoToUpdateTeam(TeamDto selectedTeam, DancerDto selectedDancer)
        {
            var parameters = new Dictionary<string, object> { { "teamDto", selectedTeam }, { "dancerDto", selectedDancer } };
            return Shell.Current.GoToAsync(UPDATE_TEAM_PAGE_ROUTE, parameters);
        }

        public Task GoToDeleteTeam(TeamDto selectedTeam)
        {
            var parameters = new Dictionary<string, object> { { "teamDto", selectedTeam } };
            return Shell.Current.GoToAsync(DELETE_TEAM_PAGE_ROUTE, parameters);
        }
        public async Task GoToDeleteDancerFromTeam(TeamDto selectedTeam, DancerDto dancerToDelete)
        {
            var parameters = new Dictionary<string, object> { { "teamDto", selectedTeam }, { "dancerDto", dancerToDelete } };
            await Shell.Current.GoToAsync(DELETE_DANCER_FROM_TEAM_PAGE_ROUTE, parameters);
        }

        public async Task GoToPostStaff()
        {
            await Shell.Current.GoToAsync(POST_STAFF_PAGE_ROUTE);
        }

        public async Task GoToSearchStaff()
        {
            await Shell.Current.GoToAsync(SEARCH_STAFF_PAGE_ROUTE);
        }

        public async Task GoToSearchStaff(bool workingWithWorkingHours)
        {
            var parameters = new Dictionary<string, object> { { "workingWithWorkingHours", workingWithWorkingHours } };
            await Shell.Current.GoToAsync(SEARCH_STAFF_PAGE_ROUTE, parameters);
        }

        

        public Task GoToUpdateStaff(StaffDto selectedStaff)
        {
            var parameters = new Dictionary<string, object> { { "staffDto", selectedStaff } };
            return Shell.Current.GoToAsync(UPDATE_STAFF_PAGE_ROUTE, parameters);
        }

        public Task GoToDeleteStaff(StaffDto selectedStaff)
        {
            var parameters = new Dictionary<string, object> { { "staffDto", selectedStaff } };
            return Shell.Current.GoToAsync(DELETE_STAFF_PAGE_ROUTE, parameters);
        }

        public Task GoToGetWorkingHours(StaffDto selectedStaff)
        {
            var parameters = new Dictionary<string, object> { { "staffDto", selectedStaff } };
            return Shell.Current.GoToAsync(GET_WORKING_HOURS_PAGE_ROUTE, parameters);
        }
    }
}



