using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class CourseCoordinator
    {
        public int Id { get; set; }

        [Required, StringLength(200), Display(Name="Co-ordinator Name")]
        public string Name { get; set; }

        [Required,StringLength(11), RegularExpression("^[0][1-9]\\d{9}$|^[1-9]\\d{9}$"), Display(Name="Mobile No")]
        public string MobileNo { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }        
        
        public List<Batch>? Batch { get; set; }
    }
}
