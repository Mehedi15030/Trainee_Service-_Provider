using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(255), Display(Name = "Course Name")]
        public string CourseName { get; set; }


        [StringLength(1000), Display(Name = "Course Description")]
        public string? CourseDescription { get; set; }

        [Required, Display(Name = "Total Hour")]
        public double TotalHours { get; set; }

        [Required, Display(Name = "Class Duration")]
        public double ClassDuration { get; set; }

        [Required, Display(Name = "Course Fee")]
        public double CourseFee { get; set; }

        [Display(Name = "Pre-requisite")]
        public string? Prerequisite { get; set; }

        public List<Batch>? Batch { get; set; }
        public List<CourseContent>? CourseContent { get; set; }
        public List<TrainerCourse>? TrainerCourses { get; set; }
    }
}
