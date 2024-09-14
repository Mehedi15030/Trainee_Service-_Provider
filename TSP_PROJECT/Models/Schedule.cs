using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        [Required, Display(Name ="Batch")]
        public int BatchId { get; set; }
        public int LabId { get; set; }

        [Required, StringLength(50), Display(Name = "Schedule Name")]
        public string ScheduleName { get; set; }

        [Required, Display(Name = "Class Date")]
        public DateTime ClassDate { get; set; }

        [Required, Display(Name = "From Time")]
        public TimeOnly FromTime { get; set; }

        [Required, Display(Name = "To Time")]
        public TimeOnly ToTime { get; set; }

        [Required, Range(0.5, 10.0)]
        public double Duration { get; set; }
        public bool IsConducted { get; set; }
        public Batch? Batch { get; set; }
        public Lab? Lab { get; set; }
    }
}
