using System.ComponentModel.DataAnnotations;

namespace GeoCV.Models
{
    public class LeggTilModel
    {
        [Required(ErrorMessage = "Studiested er påkrevd")]
        [Display(Name = "Studiested")]
        public string Studiested { get; set; }

        [Required(ErrorMessage = "Beskrivelse er påkrevd")]
        [Display(Name = "Beskrivelse")]
        public string Beskrivelse { get; set; }

        [Required(ErrorMessage = "Fra er påkrevd")]
        [Range(0, 2015, ErrorMessage = "Fra må være mellom 0 og 2015")]
        [Display(Name = "Fra")]
        public int Fra { get; set; }

        [Required(ErrorMessage = "Til er påkrevd")]
        [Range(0, 2015, ErrorMessage = "Til må være mellom 0 og 2015")]
        [Display(Name = "Til")]
        public int Til { get; set; }
    }

    public class RedigerModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Studiested er påkrevd")]
        [Display(Name = "Studiested")]
        public string Studiested { get; set; }

        [Required]
        [Display(Name = "Beskrivelse")]
        public string Beskrivelse { get; set; }

        [Required]
        [Display(Name = "Fra")]
        public int Fra { get; set; }

        [Required]
        [Display(Name = "Til")]
        public int Til { get; set; }
    }
}