using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Result
    {
        public int Id { get; set; }

        [Required, Display(Name = "Exam")]
        public int ExamId { get; set; }

        [Required, Display(Name = "Student")]
        public int StudentId { get; set; }

        [Required]
        public bool IsPresent { get; set; }

        [Required, Range(0, 1000), Display(Name = "Obtained Marks")]
        public double ObtainedMarks { get; set; }

        public Exam? Exam { get; set; }
        public Student? Student { get; set; }
    }
}
