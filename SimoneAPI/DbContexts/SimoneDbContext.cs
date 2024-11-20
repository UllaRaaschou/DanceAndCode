using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;

namespace SimoneAPI.DbContexts
{


    public class SimoneDbContext : DbContext
    {
        public DbSet<TeamDancerRelation> TeamDancerRelations { get; set; }
        public DbSet<DancerDataModel> DancerDataModels { get; set; }
        public DbSet<TeamDataModel> TeamDataModels { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<CalendarDataModel> CalendarDataModels { get; set; }

        public DbSet<WorkingHours> WorkingHours { get; set; }

        public SimoneDbContext(DbContextOptions<SimoneDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=database.db").EnableSensitiveDataLogging();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalendarDataModel>()
                .HasKey(c => c.CalendarId);

            modelBuilder.Entity<Attendance>()
              .HasKey(rl => rl.AttendanceId);

            modelBuilder.Entity<WorkingHours>()
                .HasKey(rl => rl.WorkingHoursId);

            modelBuilder.Entity<Staff>()
                .HasKey(t => t.StaffId);

            modelBuilder.Entity<Staff>()
                .HasMany(s => s.RegisteredWorkingHours)
                .WithOne(w => w.Staff)
                .HasForeignKey(rw => rw.StaffId);

            modelBuilder.Entity<DancerDataModel>()
                .HasKey(t => t.DancerId);

            modelBuilder.Entity<TeamDataModel>()
                .HasKey(t => t.TeamId);

            modelBuilder.Entity<TeamDancerRelation>()
                .HasKey(tdr => new { tdr.DancerId, tdr.TeamId });

            modelBuilder.Entity<TeamDancerRelation>()
               .HasMany(teamDancerRelation => teamDancerRelation.Attendances)
               .WithOne(a => a.TeamDancerRelation)
               .HasForeignKey(a => new { a.DancerId, a.TeamId });



            var calendarDataModel = new CalendarDataModel
            {
                CalendarId = new Guid("00000000-1111-0000-0000-000000000000"),
                SummerHolidayStart = new DateOnly(2024, 6, 27),
                SummerHolidayEnd = new DateOnly(2024, 8, 7),
                AutumnHolidayStart = new DateOnly(2024, 10, 17),
                AutumnHolidayEnd = new DateOnly(2024, 10, 21),
                ChristmasHolidayStart = new DateOnly(2024, 12, 23),
                ChristmasHolidayEnd = new DateOnly(2025, 1, 2),
                WintherHolidayStart = new DateOnly(2025, 2, 13),
                WintherHolidayEnd = new DateOnly(2025, 2, 17),
                EasterHolidayStart = new DateOnly(2025, 4, 10),
                EasterHolidayEnd = new DateOnly(2025, 4, 17),
                ChristmasShow = new DateOnly(2024, 12, 10),
                RecitalShow = new DateOnly(2025, 6, 10)
            };

            modelBuilder.Entity<CalendarDataModel>().HasData(calendarDataModel);

            modelBuilder.Entity<TeamDataModel>().HasData(
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), Number = 1, Name = "Hiphop1", ScheduledTime = "Mandag 16:00 - 16:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), Number = 2, Name = "MGP", ScheduledTime = "Tirsdag 15:15 - 16:00" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), Number = 3, Name = "Ballet", ScheduledTime = "Onsdag 17:00 - 17:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), Number = 4, Name = "Hiphop2", ScheduledTime = "Torsdag 16:00 - 16:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), Number = 5, Name = "Streetdance", ScheduledTime = "Fredag 15:00 - 15:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), Number = 6, Name = "Argentinsk Tango", ScheduledTime = "Lørdag 11:00 - 11:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), Number = 7, Name = "Salsa", ScheduledTime = "Mandag 18:00 - 18:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), Number = 8, Name = "Showdance 1", ScheduledTime = "Tirsdag 17:00 - 17:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), Number = 9, Name = "Showdance 2", ScheduledTime = "Onsdag 18:00 - 18:45" },
            new TeamDataModel(calendarDataModel) { TeamId = new Guid("07000000-0000-0000-0000-000000000000"), Number = 10, Name = "Showdance 3", ScheduledTime = "Torsdag 17:15 - 18:00" }
            );

            modelBuilder.Entity<DancerDataModel>().HasData(
               new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Mathias", TimeOfBirth = new DateOnly(2012, 5, 12) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000004"), Name = "Emma", TimeOfBirth = new DateOnly(2011, 8, 23) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000005"), Name = "Sofie", TimeOfBirth = new DateOnly(2013, 11, 19) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000006"), Name = "Lucas", TimeOfBirth = new DateOnly(2012, 3, 15) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000007"), Name = "Ida", TimeOfBirth = new DateOnly(2014, 6, 29) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000008"), Name = "Oliver", TimeOfBirth = new DateOnly(2010, 4, 20) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000009"), Name = "Maja", TimeOfBirth = new DateOnly(2013, 10, 2) },
            new DancerDataModel { DancerId = new Guid("00000000-1000-0000-0000-000000000000"), Name = "Frederik", TimeOfBirth = new DateOnly(2011, 9, 9) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000010"), Name = "Marie", TimeOfBirth = new DateOnly(2012, 7, 25) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000011"), Name = "Sebastian", TimeOfBirth = new DateOnly(2014, 5, 5) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000012"), Name = "Nora", TimeOfBirth = new DateOnly(2013, 12, 1) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000013"), Name = "Oscar", TimeOfBirth = new DateOnly(2010, 1, 17) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000014"), Name = "Clara", TimeOfBirth = new DateOnly(2012, 2, 6) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000015"), Name = "Magnus", TimeOfBirth = new DateOnly(2011, 8, 30) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000016"), Name = "Signe", TimeOfBirth = new DateOnly(2013, 3, 18) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000017"), Name = "Thea", TimeOfBirth = new DateOnly(2014, 9, 12) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000018"), Name = "Emil", TimeOfBirth = new DateOnly(2010, 5, 14) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000019"), Name = "Freya", TimeOfBirth = new DateOnly(2013, 4, 22) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000020"), Name = "Mikkel", TimeOfBirth = new DateOnly(2012, 11, 8) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000021"), Name = "Liv", TimeOfBirth = new DateOnly(2011, 7, 30) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000022"), Name = "Søren", TimeOfBirth = new DateOnly(2010, 2, 19) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000023"), Name = "Sara", TimeOfBirth = new DateOnly(2013, 6, 4) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000024"), Name = "Joakim", TimeOfBirth = new DateOnly(2012, 3, 26) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000025"), Name = "Tilde", TimeOfBirth = new DateOnly(2014, 10, 30) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000026"), Name = "Mikkel", TimeOfBirth = new DateOnly(2012, 11, 8) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000027"), Name = "Liv", TimeOfBirth = new DateOnly(2011, 7, 30) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000028"), Name = "Søren", TimeOfBirth = new DateOnly(2010, 2, 19) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000029"), Name = "Sara", TimeOfBirth = new DateOnly(2013, 6, 4) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000030"), Name = "Joakim", TimeOfBirth = new DateOnly(2012, 3, 26) },
            new DancerDataModel { DancerId = new Guid("00000000-0000-0000-0000-000000000031"), Name = "Tilde", TimeOfBirth = new DateOnly(2014, 10, 30) }
            );


            modelBuilder.Entity<TeamDancerRelation>().HasData(
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000003"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000004"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000005"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000006"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000007"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000008"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000009"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000010"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000011"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000012"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000013"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000014"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000015"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000016"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000017"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000018"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000019"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000020"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000021"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000022"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000023"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000024"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000025"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000026"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000027"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000028"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000029"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000030"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000031"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000009"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000022"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000011"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000016"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000022"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000015"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000018"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000017"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000025"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000030"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000028"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000029"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000023"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000014"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000027"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000029"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000014"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000009"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000008"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000004"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000006"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000025"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000018"), IsTrialLesson = false, LastDanceDate = new DateOnly(2025, 6, 10) }


                     );



            base.OnModelCreating(modelBuilder);
        }

    }
}

