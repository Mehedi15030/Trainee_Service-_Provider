using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required, Display(Name = "Batch")]
        public int BatchId  { get; set; }

 
        [Required, Display(Name = "Student")]
        public int StudentId { get; set; }  


        [Required]
        public DateTime PaymentDate { get; set; }

        [Required, StringLength(30), Display(Name = "Reciept No")]
        public string RecieptNo { get; set; }

        [Required]
        public bool IsFullPayment { get; set; }

        [Required, Range(0, 999999)]
        public double Amount { get; set; }

        public Batch? Batch { get; set; }
        public Student? Student { get; set; }
    }
}
