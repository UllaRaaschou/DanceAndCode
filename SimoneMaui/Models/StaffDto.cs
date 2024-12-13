namespace SimoneMaui.Models
{
    public class StaffDto
    {
        public Guid? StaffId { get; set; }
        public string Name { get; set; } = string.Empty;
        public MauiJobRoleEnum Role { get; set; }
        public DateOnly TimeOfBirth { get; set; }
    }
}
