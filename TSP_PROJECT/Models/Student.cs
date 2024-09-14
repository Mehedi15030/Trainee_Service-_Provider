using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, StringLength(250), Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Required, StringLength(500), Display(Name ="Student Address")]
        public string StudentAddress { get; set; }

        [Required, StringLength(11), RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$"), Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        [StringLength(200)]
        public string? EducationalQualification { get; set; }

        public List<Batch>? Batches { get; set; }

    }
}
