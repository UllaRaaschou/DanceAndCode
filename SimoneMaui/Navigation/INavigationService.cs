﻿using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.Navigation;
public interface INavigationService
{
    Task GoToSearchDancer();
    Task GoToSearchDancer(TeamDto teamDto, bool puttingDancerOnTeam);
    Task GoToUpdateDancer(DancerDto dancerDto);
    Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto);
    Task GoToSearchTeam(DancerDto dancerDto);
    Task GoToUpdateTeam(TeamDto selectedTeam);
    Task GoToUpdateTeam(TeamDto selectedTeam, DancerDto selectedDancer);
    Task GoToSearchTeam();
    Task GoToDeleteTeam(TeamDto selectedTeam);


}

