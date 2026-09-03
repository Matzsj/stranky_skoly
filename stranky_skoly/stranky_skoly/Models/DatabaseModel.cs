using System.ComponentModel.DataAnnotations;

namespace stranky_skoly.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string DayOfWeek { get; set; }
        public string TimeSlot { get; set; }
        public string Subject { get; set; }
        public string Teacher { get; set; }
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClassName { get; set; }
        public string Email { get; set; }
    }
}
