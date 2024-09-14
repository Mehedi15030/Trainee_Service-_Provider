using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        [Required, StringLength(200), Display(Name = "Trainer Name")]
        public string TrainerName { get; set; }

        [Required, StringLength(11), RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$"), Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(200)]
        public string Expertise { get; set; }

        [StringLength(1000)]
        public string? ShortBio { get; set; }

        public List<TrainerCourse>? TrainerCourses { get; set; }
    }
}
