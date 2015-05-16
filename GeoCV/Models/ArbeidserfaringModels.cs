using System.ComponentModel.DataAnnotations;

namespace GeoCV.Models
{
    public class ArbeidserfaringModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Arbeidsplass er påkrevd")]
        [Display(Name = "Arbeidsplass")]
        public string Arbeidsplass { get; set; }

        [Required(ErrorMessage = "Stilling er påkrevd")]
        [Display(Name = "Stilling")]
        public string Stilling { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrevd")]
        [Display(Name = "Beskrivelse")]
        public string Beskrivelse { get; set; }

        [Required(ErrorMessage = "Nåværende stilling er påkrevd")]
        [Display(Name = "Nåværende stilling")]
        public bool NåværendeStilling { get; set; }

        [Required(ErrorMessage = "Fra er påkrevd")]
        [Range(0, 2015, ErrorMessage = "Fra må være mellom 0 og 2015")]
        [Display(Name = "Fra")]
        public int Fra { get; set; }

        [Required(ErrorMessage = "Til er påkrevd")]
        [Range(0, 2015, ErrorMessage = "Til må være mellom 0 og 2015")]
        [Display(Name = "Til")]
        public int Til { get; set; }
    }

    public class SlettArbeidserfaringModel
    {
        [Required]
        public int Id { get; set; }
    }
}