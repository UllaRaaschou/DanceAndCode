﻿using SimoneMaui.ViewModels.Dtos;

namespace SimoneMaui.Navigation;
public interface INavigationService
{
    Task GoToFirstPage();
    Task GoToPostDancer();

    Task GoToSearchDancer();
    Task GoToSearchDancer(TeamDto teamDto, bool puttingDancerOnTeam);

    Task GoToUpdateDancer(DancerDto dancerDto);
    Task GoToUpdateDancer(DancerDto dancerDto, TeamDto teamDto, bool wannaAdd, bool wannaAddTrial);

    Task GoToDeleteDancer(DancerDto dancerDto);




    Task GoToPostTeam();
    Task GoToSearchTeam(DancerDto dancerDto);
    Task GoToSearchTeam(DancerDto dancerDto, bool wannaAddTeamToADancer, bool wannaAddTeamTrialLesson);
    Task GoToUpdateTeam(TeamDto selectedTeam);
    Task GoToUpdateTeam(TeamDto selectedTeam, DancerDto selectedDancer);
    Task GoToSearchTeam();
    Task GoToDeleteTeam(TeamDto selectedTeam);

    Task GoToDeleteDancerFromTeam(TeamDto selectedTeam, DancerDto dancerToDelete);


    
    Task GoToPostStaff();
    Task GoToSearchStaff();
    Task GoToUpdateStaff(StaffDto staffDto);
    Task GoToDeleteStaff(StaffDto staffDto);


}

