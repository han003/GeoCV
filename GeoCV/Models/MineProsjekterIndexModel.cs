using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class MineProsjekterIndexModel
    {
        public IEnumerable<Prosjekt> Prosjekt { get; set; }
        public IEnumerable<ListeKatalog> Stillinger { get; set; }
        public IEnumerable<MineProsjekterObjekt> BrukerProsjekter { get; set; }
        public IEnumerable<ListeKatalog> Katalog { get; set; }
    }
}