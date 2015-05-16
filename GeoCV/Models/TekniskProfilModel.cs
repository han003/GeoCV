using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoCV.Models
{
    public class TekniskProfilModel
    {
        public IEnumerable<Prosjekt> Prosjekter { get; set; }
        public IEnumerable<ListeKatalog> KatalogElementer { get; set; }
        public IEnumerable<TekniskProfil> TekniskeProfiler { get; set; }
    }

    public class SlettTekniskProfilModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class LeggTilTekniskProfilModel
    {
        [Required]
        public int ProsjektId { get; set; }

        [Required]
        [Display(Name = "Profil navn")]
        public string ProfilNavn { get; set; }
    }
}