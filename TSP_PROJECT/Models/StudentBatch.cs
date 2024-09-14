using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class StudentBatch
    {
        public int Id { get; set; }

        [Required, Display(Name ="Batch")]
        public int BatchId { get; set; }
        
        [Required, Display(Name ="Student")]
        public int StudentId { get; set; }

        public Batch? Batch { get; set; }
        public Student? Student { get; set; }
        public List<Payment>? Payments { get; set; }
    }
}
