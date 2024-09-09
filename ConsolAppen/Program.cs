using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimoneAPI.DbContexts;
using SimoneAPI.Entities;

class Program
{
    static void Main(string[] args)
    {
        // Configure Dependency Injection
        var serviceProvider = new ServiceCollection()
            //.AddDbContext<SimoneDbContext>(options =>
            //    options.UseSqlite("Data Source=database.db"))  
            .BuildServiceProvider();




        using (var context = serviceProvider.GetRequiredService<SimoneDbContext>())
        {
            context.Database.Migrate();

            var dancer1 = new Dancer { DancerId = Guid.NewGuid(), Name = "Petra" };
            var dancer2 = new Dancer { DancerId = Guid.NewGuid(), Name = "Silje" };

            var Team1 = new Team { ClassId = Guid.NewGuid(), Name = "Hiphop1", SceduledTime = "Mandag 16:00 - 16:45" };
            var Team2 = new Team { ClassId = Guid.NewGuid(), Name = "MGP", SceduledTime = "Tirsdag 15:15 - 16:00" };

            Team1.EnrolledDancers.Add(dancer1);
            Team1.EnrolledDancers.Add(dancer2);

            Team2.EnrolledDancers.Add(dancer1);

            dancer1.RegisteredTeams.Add(Team1);
            dancer1.RegisteredTeams.Add(Team2);
            dancer2.RegisteredTeams.Add(Team1);

            try
            {
                context.Dancers.Add(dancer1);
                context.Dancers.Add(dancer2);
                context.Classes.Add(Team1);
                context.Classes.Add(Team2);
                context.SaveChanges(true);


                var teams = context.Classes.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving changes: {ex.Message}");
            }

        }




    }


}
