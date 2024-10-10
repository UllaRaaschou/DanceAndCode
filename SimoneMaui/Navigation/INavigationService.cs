using SimoneMaui.ViewModels;

namespace SimoneMaui.Navigation;
public interface INavigationService
{
    Task GoToSearchDancer();
    Task GoToUpdateDancer(DancerDto dancerDto);
    Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto);

    Task GoToSearchTeam(DancerDto dancerDto);
}

