using Microsoft.EntityFrameworkCore;
using SimoneAPI.DataModels;

namespace SimoneAPI.DbContexts
{


    public class SimoneDbContext: DbContext
    {
        public DbSet<TeamDancerRelation> TeamDancerRelations { get; set; }
        public  DbSet<DancerDataModel> DancerDataModels { get; set; }
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
            modelBuilder.Entity<WorkingHours>()
                .HasKey(rl => rl.WorkingHoursId);

            modelBuilder.Entity<Staff>()
                .HasKey(t => t.StaffId);

            modelBuilder.Entity<Staff>()
                .HasMany(t => t.RegisteredWorkingHours)
                .WithOne(rl => rl.Staff)
                .HasForeignKey(rl => rl.StaffId);

            modelBuilder.Entity<TeamDancerRelation>()
                .HasKey(tdr =>  new { tdr.DancerId, tdr.TeamId });

            //modelBuilder.Entity<TeamDancerRelation>()
            //    .HasMany(tdr => tdr.Attendances)
            //    .WithOne(a => a.TeamDancerRelation)
            //    .HasForeignKey(a => a.TeamDancerRelationId);

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

            modelBuilder.Entity<CalendarDataModel>()
                .HasKey(c => c.CalendarId);

           var calendarDataModel = new CalendarDataModel
               {
                    CalendarId = new Guid("00000000-1111-0000-0000-000000000000"),
                    SummerHolidayStart = new DateOnly(2022, 6, 27),
                    SummerHolidayEnd = new DateOnly(2022, 8, 7),
                    AutumnHolidayStart = new DateOnly(2022, 10, 17),
                    AutumnHolidayEnd = new DateOnly(2022, 10, 21),
                    ChristmasHolidayStart = new DateOnly(2022, 12, 23),
                    ChristmasHolidayEnd = new DateOnly(2023, 1, 2),
                    WintherHolidayStart = new DateOnly(2023, 2, 13),
                    WintherHolidayEnd = new DateOnly(2023, 2, 17),
                    EasterHolidayStart = new DateOnly(2023, 4, 10),
                    EasterHolidayEnd = new DateOnly(2023, 4, 17),
                    ChristmasShow = new DateOnly(2022, 12, 10),
                    RecitalShow = new DateOnly(2023, 6, 10)
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
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111100"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000003") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111101"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000004") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111102"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000005") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111103"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000006") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111104"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000007") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111105"), TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000008") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111106"), TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000009") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111107"), TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000010") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111108"), TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000011") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111109"), TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000012") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111110"), TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000013") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111111"), TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000014") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111112"), TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000015") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111113"), TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000016") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111114"), TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000017") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111115"), TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111116"), TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000019") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111117"), TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000020") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111118"), TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000021") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111119"), TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000022") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111120"), TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000023") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111121"), TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000024") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111122"), TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000025") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111123"), TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000026") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111124"), TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000027") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111125"), TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000028") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111126"), TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000029") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111127"), TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000030") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-111111111128"), TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000031") },

            //new TeamDancerRelation { TeamDancerRelationId = new Guid("11111111-1111-1111-1111-711111111101"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000007") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111102"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000009") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("31111111-1111-1111-1111-111111111103"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000022") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("41111111-1111-1111-1111-111111111104"), TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000011") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("51111111-1111-1111-1111-111111111105"), TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000016") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("61111111-1111-1111-1111-111111111106"), TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000022") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("71111111-1111-1111-1111-111111111107"), TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000015") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("81111111-1111-1111-1111-111111111108"), TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("91111111-1111-1111-1111-111111111109"), TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000017") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("10111111-1111-1111-1111-111111111110"), TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("13111111-1111-1111-1111-111111111111"), TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000025") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("14111111-1111-1111-1111-111111111112"), TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000030") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("15111111-1111-1111-1111-111111111113"), TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000028") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("16111111-1111-1111-1111-111111111114"), TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000029") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("17111111-1111-1111-1111-111111111115"), TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000023") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("18111111-1111-1111-1111-111111111116"), TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000014") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("19111111-1111-1111-1111-111111111117"), TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000027") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("20111111-1111-1111-1111-111111111118"), TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000027") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111119"), TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000021") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111120"), TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000029") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111121"), TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000023") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111122"), TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000014") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111123"), TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000009") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111124"), TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000008") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111125"), TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000004") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111126"), TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000006") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111127"), TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000025") },
            //new TeamDancerRelation { TeamDancerRelationId = new Guid("21111111-1111-1111-1111-111111111128"), TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") }




            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000003") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000004") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000005") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000006") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000007") },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000008") },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000009") },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000010") },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000011") },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000012") },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000013") },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000014") },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000015") },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000016") },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000017") },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000019") },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000020") },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000021") },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000022") },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000023") },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000024") },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000025") },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000026") },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000027") },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000028") },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000029") },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000030") },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000031") },

            //new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000007") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000009") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000022") },
            new TeamDancerRelation { TeamId = new Guid("11111111-1111-1111-1111-111111111111"), DancerId = new Guid("00000000-0000-0000-0000-000000000011") },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000016") },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000022") },
            new TeamDancerRelation { TeamId = new Guid("22222222-2222-2222-2222-222222222222"), DancerId = new Guid("00000000-0000-0000-0000-000000000015") },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") },
            new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000017") },
            //new TeamDancerRelation { TeamId = new Guid("33333333-3333-3333-3333-333333333333"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000025") },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000030") },
            new TeamDancerRelation { TeamId = new Guid("44444444-4444-4444-4444-444444444444"), DancerId = new Guid("00000000-0000-0000-0000-000000000028") },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000029") },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000023") },
            new TeamDancerRelation { TeamId = new Guid("55555555-5555-5555-5555-555555555555"), DancerId = new Guid("00000000-0000-0000-0000-000000000014") },
            new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000027") },
            //new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000027") },
            //new TeamDancerRelation { TeamId = new Guid("66666666-6666-6666-6666-666666666666"), DancerId = new Guid("00000000-0000-0000-0000-000000000021") },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000029") },
            //new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000023") },
            new TeamDancerRelation { TeamId = new Guid("77777777-7777-7777-7777-777777777777"), DancerId = new Guid("00000000-0000-0000-0000-000000000014") },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000009") },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000008") },
            new TeamDancerRelation { TeamId = new Guid("88888888-8888-8888-8888-888888888888"), DancerId = new Guid("00000000-0000-0000-0000-000000000004") },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000006") },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000025") },
            new TeamDancerRelation { TeamId = new Guid("99999999-9999-9999-9999-999999999999"), DancerId = new Guid("00000000-0000-0000-0000-000000000018") }


                     );
        


            base.OnModelCreating(modelBuilder);
        }

    }
}
