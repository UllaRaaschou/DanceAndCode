
using SimoneMaui.ViewModels;
using SimoneMaui.Views;
using static SimoneMaui.ViewModels.SearchDancerViewmodel;
using SimoneMaui.ViewModels.Dtos;
using SimoneMaui.Views.TeamViews;

namespace SimoneMaui.Navigation;

public class NavigationService : INavigationService
{
    public const string UPDATE_DANCER_PAGE_ROUTE = "updateDancer";
    public const string SEARCH_DANCER_PAGE_ROUTE = "searchDancer";
    public const string DELETE_DANCER_PAGE_ROUTE = "deleteDancer";
   

    public const string SEARCH_TEAM_PAGE_ROUTE = "searchTeam";
    public const string UPDATE_TEAM_PAGE_ROUTE = "updateTeam";
    public const string DELETE_TEAM_PAGE_ROUTE = "deleteTeam";
    public const string DELETE_DANCER_FROM_TEAM_PAGE_ROUTE = "deleteDancerFromTeam";

    public static void ConfigureRouter()
    {
        Routing.RegisterRoute(UPDATE_DANCER_PAGE_ROUTE, type: typeof(UpdateDancerPage));
        Routing.RegisterRoute(SEARCH_DANCER_PAGE_ROUTE, type: typeof(SearchDancerPage));
        Routing.RegisterRoute(DELETE_DANCER_PAGE_ROUTE, type: typeof (DeleteDancerPage));
        Routing.RegisterRoute(DELETE_DANCER_FROM_TEAM_PAGE_ROUTE, type: typeof(DeleteDancerFromTeamPage));

        Routing.RegisterRoute(SEARCH_TEAM_PAGE_ROUTE, type: typeof(SearchTeamPage));
        Routing.RegisterRoute(UPDATE_TEAM_PAGE_ROUTE, type: typeof(UpdateTeamPage));
        Routing.RegisterRoute(DELETE_TEAM_PAGE_ROUTE, type: typeof(DeleteTeamPage));
    }

    public async Task GoToUpdateDancer(DancerDto dancerDto)
    {
        var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto } };
        await Shell.Current.GoToAsync(UPDATE_DANCER_PAGE_ROUTE, parameters);
    }

    public async Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto)
    {
        var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto }, { "teamDto", teamDto } };
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
    public async Task GoToSearchDancer(TeamDto teamDto, bool puttingDancerOnTeam)
    {
        var parameters = new Dictionary<string, object> { { "teamDto", teamDto }, { "puttingDancerOnTeam", puttingDancerOnTeam } };
        await Shell.Current.GoToAsync(SEARCH_DANCER_PAGE_ROUTE, parameters);
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
    public async Task GoToSearchTeam(DancerDto dancerDto, bool wannaAddTeamToADancer)
    {
        var parameters = new Dictionary<string, object> { { "dancerDto", dancerDto }, { "WannaAddTeamToADancer", wannaAddTeamToADancer } };
        await Shell.Current.GoToAsync(SEARCH_TEAM_PAGE_ROUTE, parameters);
    }

    public Task GoToUpdateTeam(TeamDto selectedTeam)
    {
        var parameters = new Dictionary<string, object> { { "teamDto", selectedTeam } };
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


}
