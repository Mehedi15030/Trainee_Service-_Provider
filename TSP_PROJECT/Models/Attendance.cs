using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required, Display(Name = "Schedule")]
        public int ScheduleId { get; set; }

        
        [Required, Display(Name = "Student")]
        public int StudentId { get; set; }


        [Required]
        public bool IsPresent { get; set; }

        public Schedule? Schedule { get; set; }
        public Student? Student { get; set; }
    }
}
