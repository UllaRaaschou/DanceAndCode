using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;

namespace SimoneAPI.DbContexts
{


    public class SimoneDbContext: DbContext
    {
        public DbSet<TeamDancerRelation> TeamDancerRelations { get; set; }
        public  DbSet<DancerDataModel> DancerDataModels { get; set; }
        public DbSet<TeamDataModel> TeamDataModels { get; set; }
       
        public SimoneDbContext(DbContextOptions<SimoneDbContext> options) : base(options)
        {
       
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=database.db").EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<TeamDancerRelation>()
                .HasKey(tdr => tdr.TeamDancerRelationId);

            modelBuilder.Entity<TeamDataModel>()
                .HasKey(t => t.TeamId);


            modelBuilder.Entity<TeamDataModel>()
                .HasMany(t => t.TeamDancerRelations)
                .WithOne(tdr => tdr.TeamDataModel)   
                .HasForeignKey(tdr => tdr.TeamId);

            modelBuilder.Entity<DancerDataModel>()
                .HasKey(d => d.DancerId);

            modelBuilder.Entity<DancerDataModel>()
                .HasMany(d => d.TeamDancerRelations)
                .WithOne(tdr => tdr.DancerDataModel)
                .HasForeignKey(tdr => tdr.DancerId);

          
            modelBuilder.Entity<TeamDataModel>().HasData(
                new TeamDataModel { TeamId = Guid.Parse("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), Number = 1, Name = "Hiphop1", SceduledTime = "Mandag 16:00 - 16:45" },
                new TeamDataModel { TeamId = Guid.Parse("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), Number = 2, Name = "MGP", SceduledTime = "Tirsdag 15:15 - 16:00" }
            );

            modelBuilder.Entity<DancerDataModel>().HasData(
                new DancerDataModel { DancerId = Guid.Parse("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), Name = "Petra" },
                new DancerDataModel { DancerId = Guid.Parse("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"), Name = "Silje" }
            );

            modelBuilder.Entity<TeamDancerRelation>().HasData(
            new TeamDancerRelation
            {
                TeamDancerRelationId = Guid.NewGuid(),
                TeamId = Guid.Parse("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                DancerId = Guid.Parse("c5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48")
            },
            new TeamDancerRelation
            {
                TeamDancerRelationId = Guid.NewGuid(),
                TeamId = Guid.Parse("b5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                DancerId = Guid.Parse("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48")
            },
            new TeamDancerRelation
            {
                TeamDancerRelationId = Guid.NewGuid(),
                TeamId = Guid.Parse("a5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48"),
                DancerId = Guid.Parse("d5f15d2a-8f60-4d1b-b7b5-c0aeb10a4e48")

            }
            );




            base.OnModelCreating(modelBuilder);
        }







        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Team>()
        //     .HasMany(t => t.EnrolledDancers)
        //     .WithMany(d => d.RegisteredTeams)
        //     .UsingEntity<DancerTeamRelation>(
        //        j => j.HasOne(dtr => dtr.Dancer) // Navigations-egenskab til Dancer
        //       .WithMany(d => d.DancerTeamRelations) // Navigations-egenskab i Dancer
        //        .HasForeignKey(dtr => dtr.DancerId),
        //         j => j.HasOne(dtr => dtr.Team) // Navigations-egenskab til Team
        //         .WithMany(t => t.DancerTeamRelations) // Navigations-egenskab i Team
        //  .     HasForeignKey(dtr => dtr.TeamId));

        //    var team1 = new Team { TeamId = Guid.Parse("a973a8c4-b739-4f8a-846e-28e0170b750e"), Name = "Hiphop1", SceduledTime = "Mandag 16:00 - 16:45" };
        //    var Team2 = new Team { TeamId = Guid.Parse("b973a8c4-b739-4f8a-846e-28e0170b750e"), Name = "MGP", SceduledTime = "Tirsdag 15:15 - 16:00" };
        //    modelBuilder.Entity<Team>().HasData(team1, Team2);

        //    var dancer1 = new Dancer { DancerId = Guid.Parse("c973a8c4-b739-4f8a-846e-28e0170b750e"), Name = "Petra" };
        //    var dancer2 = new Dancer { DancerId = Guid.Parse("d973a8c4-b739-4f8a-846e-28e0170b750e"), Name = "Silje" };
        //    modelBuilder.Entity<Dancer>().HasData(dancer1, dancer2);

        //    modelBuilder.Entity<DancerTeamRelation>().HasData(
        //        new DancerTeamRelation() { DancerId = Guid.Parse("c973a8c4-b739-4f8a-846e-28e0170b750e"), TeamId = Guid.Parse("a973a8c4-b739-4f8a-846e-28e0170b750e") },
        //        new DancerTeamRelation() { DancerId = Guid.Parse("d973a8c4-b739-4f8a-846e-28e0170b750e"), TeamId = Guid.Parse("a973a8c4-b739-4f8a-846e-28e0170b750e") },
        //        new DancerTeamRelation() { DancerId = Guid.Parse("d973a8c4-b739-4f8a-846e-28e0170b750e"), TeamId = Guid.Parse("b973a8c4-b739-4f8a-846e-28e0170b750e") }
        //    );
        //}
    }
}
