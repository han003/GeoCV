
using System.ComponentModel.DataAnnotations;
namespace GeoCV.Models
{
    public class ProsjekterModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kunde er påkrevd")]
        [Display(Name = "Kunde")]
        public string Kunde { get; set; }

        [Required(ErrorMessage = "Prosjektnavn er påkrevd")]
        [Display(Name = "Prosjektnavn")]
        public string Prosjektnavn { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrevd")]
        [Display(Name = "Beskrivelse")]
        public string Beskrivelse { get; set; }
    }

    public class SlettProsjektModel
    {
        [Required]
        public int Id { get; set; }
    }
}