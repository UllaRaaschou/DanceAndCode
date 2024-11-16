using SimoneBlazor.Domain;
using System.Runtime.ConstrainedExecution;
using System;

namespace SimoneBlazor.Components.Services
{
    public class MockDataService
    {
        private static List<TeamBlazor>? _teams = default!;

        public static List<TeamBlazor>? Teams
        {
            get
            {
                _teams ??= ÍnitializeMockTeams();
                return _teams;
            }            
        }

        private static List<TeamBlazor> ÍnitializeMockTeams() 
        {
            var team1 = new TeamBlazor
            {
                TeamId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb6"),
                Number = 1,
                Name = "Team 1",
                ScheduledTime = "Mandag 16:00 - 18:00",
                DayOfWeek = DayOfWeek.Monday,
                DancersOnTeam = new List<DancerBlazor>
                {
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb1"),
                        Name = "Dancer 1"
                    },
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb2"),
                        Name = "Dancer 2"
                    },
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb3"),
                        Name = "Dancer 3"
                    }
                }
            };



            var team2 = new TeamBlazor
            {
                TeamId = new Guid("7f1c7b8e-2e04-4d25-9308-7f27d9af9cb6"),
                Number = 2,
                Name = "Team 2",
                ScheduledTime = "Tirsdag 16:00 - 18:00",
                DayOfWeek = DayOfWeek.Tuesday,
                DancersOnTeam = new List<DancerBlazor>
                {
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb4"),
                        Name = "Dancer 4"
                    },
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb5"),
                        Name = "Dancer 5" },
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb6"),
                        Name = "Dancer 6" }
                }
            };


            var team3 = new TeamBlazor
            {
                TeamId = new Guid("1f1c7b8e-2e04-4d25-9308-7f27d9af9cb6"),
                Number = 3,
                Name = "Team 3",
                ScheduledTime = "Onsdag 16:00 - 18:00",
                DayOfWeek = DayOfWeek.Wednesday,
                DancersOnTeam = new List<DancerBlazor>
                {
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb7"),
                        Name = "Dancer 7" },
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb8"),
                        Name = "Dancer 8" },
                    new DancerBlazor
                    {
                        DancerId = new Guid("9f1c7b8e-2e04-4d25-9308-7f27d9af9cb9"),
                        Name = "Dancer 9" }
                }
            };
            return [team1, team2, team3];
        }
    }
}
