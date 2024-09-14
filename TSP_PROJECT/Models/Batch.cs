using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Batch
    {
        public int Id { get; set; }

        [Required, Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required, Display(Name = "Trainer")]
        public int TrainerId { get; set; }  

        [Required, StringLength(200), Display(Name = "Batch Name")]
        public string BatchName { get; set; }

        [StringLength(50), Display(Name = "Batch Short Name")]
        public string BatchShortName { get; set; }

        [Required, Display(Name ="Course Co-ordinator")]
        public int CourseCoordinatorId { get; set; }
        
        
        public Course? Course { get; set; }
        public Trainer? Trainer { get; set; }      
        public CourseCoordinator? CourseCoordinator { get; set; }
        public List<Schedule>? Schedule { get; set; }
        public List<Exam>? Exam { get; set; }
        public List<StudentBatch>? StudentBatch { get; set; }
    }
}
