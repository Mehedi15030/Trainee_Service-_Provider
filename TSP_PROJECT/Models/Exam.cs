using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Exam
    {
        public int Id { get; set; }

        [Required, Display(Name = "Batch")]
        public int BatchId { get; set; }

        [Required, StringLength(50)]
        public string Topic { get; set; }

        [Required, Range(1, 1000), Display(Name = "Full Marks")]
        public double FullMarks { get; set; }
        
        [Display(Name = "Exam Date")]
        public DateTime ExamDate { get; set; }

        [Display(Name = "Exam Time")]
        public string? ExamTime { get; set; }

        [Display(Name = "Is Online Exam")]
        public bool IsOnline { get; set; }
        public Batch? Batch { get; set; }
    }
}
