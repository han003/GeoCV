using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class ExpertiseModel
    {
        public IEnumerable<ListeKatalog> Katalog { get; set; }
        public IEnumerable<ListeKatalog> BrukerProgrammeringsspråk { get; set; }
        public IEnumerable<ListeKatalog> BrukerRammeverk { get; set; }
        public IEnumerable<ListeKatalog> BrukerWebteknologier { get; set; }
        public IEnumerable<ListeKatalog> BrukerDatabasesystemer { get; set; }
        public IEnumerable<ListeKatalog> BrukerServerside { get; set; }
        public IEnumerable<ListeKatalog> BrukerOperativsystemer { get; set; }
        public IEnumerable<ListeKatalog> BrukerAnnet { get; set; }
    }
}