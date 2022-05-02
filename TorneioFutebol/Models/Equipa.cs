using System.ComponentModel.DataAnnotations;

namespace TorneioFutebol.Models
{
    public class Equipa
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
        [Required]
        public string Captain { get; set; }
        [Display(Name = "Number Of Members")]
        public int NumberOfMembers { get; set; }
    }
}
