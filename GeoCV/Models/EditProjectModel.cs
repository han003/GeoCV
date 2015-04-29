using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class EditProjectModel
    {
        public Prosjekt Prosjekt { get; set; }
        public IEnumerable<ListeKatalog> KatalogElementer { get; set; }
        public IEnumerable<TekniskProfil> TekniskeProfiler { get; set; }
    }
}