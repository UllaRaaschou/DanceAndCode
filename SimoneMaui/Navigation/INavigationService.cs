using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.Navigation;
public interface INavigationService
{
    Task GoToSearchDancer();
    Task GoToSearchDancer(TeamDto teamDto, bool puttingDancerOnTeam);

    Task GoToUpdateDancer(DancerDto dancerDto);
    Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto);
    Task GoToDeleteDancer(DancerDto dancerDto);



    Task GoToDeleteDancerFromTeam(TeamDto selectedTeam, DancerDto dancerToDelete);

    Task GoToSearchTeam(DancerDto dancerDto);
    Task GoToSearchTeam(DancerDto dancerDto, bool wannaAddTeamToADancer);
    Task GoToUpdateTeam(TeamDto selectedTeam);
    Task GoToUpdateTeam(TeamDto selectedTeam, DancerDto selectedDancer);
    Task GoToSearchTeam();
    Task GoToDeleteTeam(TeamDto selectedTeam);


}

