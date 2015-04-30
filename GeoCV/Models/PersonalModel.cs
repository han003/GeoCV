using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class PersonalModel
    {
        public CVVersjon BrukerCv { get; set; }
        public IEnumerable<ListeKatalog> Språk { get; set; }
        public IEnumerable<ListeKatalog> BrukerSpråk { get; set; }
        public IEnumerable<ListeKatalog> Nasjonaliteter { get; set; }
        public IEnumerable<ListeKatalog> Stillinger { get; set; }
    }
}