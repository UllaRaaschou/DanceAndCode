using SimoneAPI.DbContexts;

namespace SimoneAPI.DataModels
{
    public class Staff
    {
        public Guid StaffId { get; set; }
        public string Name { get; set; }
        public JobRoleEnum Role { get; set; }
        public DateTime TimeofBirth { get; set; }
        public ICollection<RegisteredLesson> RegisteredLessons { get; set; } = new HashSet<RegisteredLesson>(); 

        //    public async Task<IResult> GetCountOfRegisteredLessons(SimoneDbContext dbContext, Guid staffId, DateTime countStartDate, DateTime countEndDate)
        //    {
        //        var count = 0;

        //        var lessons = await dbContext.Teachers
        //           .Include(t => t.RegisteredLessons)
        //           .FirstOrDefaultAsync(t => t.TeacherId == teacherId);

        //        if (lessons == null)
        //        {
        //            return TypedResults.NoContent();
        //        }


        //        foreach (var lesson in lessons.RegisteredLessons)
        //        {

        //            if (lesson.Date >= countStartDate && lesson.Date <= countEndDate)
        //            {
        //                count++;
        //            }
        //        }

        //        return TypedResults.Ok(count);
        //    }
        //}
    }
}
