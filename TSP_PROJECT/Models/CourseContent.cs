using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class CourseContent
    {
        public int Id { get; set; }

        [Required, Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required, StringLength(150), Display(Name ="Content Title")]
        public string ContentTitle { get; set; }

        [Required, StringLength(1000), Display(Name = "Content Detail")]
        public string ContentDetail { get; set; }

        [Required, Range(1,100), Display(Name = "Sort Order")]
        public int SortOrder { get; set; }

        public Course? Course { get; set; }
    }
}
