using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class MineProsjekterLeggTilModel
    {
        public IEnumerable<Prosjekt> AlleProsjekter { get; set; }
        public IEnumerable<Medlem> BrukerProsjekter { get; set; }
    }
}