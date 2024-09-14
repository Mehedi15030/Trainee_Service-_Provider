using System.ComponentModel.DataAnnotations;

namespace TSP_PROJECT.Models
{
    public class Lab
    {
        public int Id { get; set; }

        [Required,StringLength(50), Display(Name ="Lab Name")]
        public string LabName { get; set; }

        [StringLength(20), Display(Name = "Lab Short Name")]
        public string LabShortName { get; set; }

        List<Schedule>? Schedule { get; set; }
    }
}
