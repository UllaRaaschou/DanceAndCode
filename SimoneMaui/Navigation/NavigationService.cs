
using SimoneMaui.ViewModels;
using SimoneMaui.Views;

namespace SimoneMaui.Navigation;

public class NavigationService : INavigationService
{
    public const string UPDATE_DANCER_PAGE_ROUTE = "updatedancer";
    public const string SEARCH_DANCER_PAGE_ROUTE = "searchdancer";
    public const string SEARCH_TEAM_PAGE_ROUTE = "searchteam";
    public static void ConfigureRouter()
    {
        Routing.RegisterRoute(UPDATE_DANCER_PAGE_ROUTE, type: typeof(UpdateDancerPage));
        Routing.RegisterRoute(SEARCH_DANCER_PAGE_ROUTE, type: typeof(SearchDancerPage));
        Routing.RegisterRoute(SEARCH_TEAM_PAGE_ROUTE, type: typeof(SearchTeamPage));
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

    public async Task GoToSearchDancer()
    {
        await Shell.Current.GoToAsync(SEARCH_DANCER_PAGE_ROUTE);
    }

    public async Task GoToSearchTeam(DancerDto dancerDto)
    {
        await Shell.Current.GoToAsync(SEARCH_TEAM_PAGE_ROUTE);
    }
}
