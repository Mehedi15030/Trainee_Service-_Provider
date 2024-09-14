using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class TrainerCourse
    {
        public int Id { get; set; }

        [Required, Display(Name ="Course Name")]
        public int CourseId { get; set; }

        [Required, Display(Name ="Trainer Name")]
        public int TrainerId { get; set; }

        [Display(Name = "Is Available ?")]
        public bool IsAvailable { get; set; }

        public Course? Course { get; set; }
        public Trainer? Trainer { get; set; }
    }
}
