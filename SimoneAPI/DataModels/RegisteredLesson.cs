namespace SimoneAPI.DataModels
{
    public class RegisteredLesson
    {
        public Guid LessonId { get; set; }
        public DateTime Date { get; set; } 
        public Guid TeamId { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }

       
    }
}