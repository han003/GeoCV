using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class MineProsjekterObjekt
    {
        public int ProsjektId { get; set; }
        public string ProsjektKunde { get; set; }
        public string ProsjektNavn { get; set; }

        public IEnumerable<TekniskProfil> ProsjektTekniskProfil { get; set; }

        public int MedlemId { get; set; }
        public int? MedlemRolle { get; set; }
        public int? MedlemTekniskProfil { get; set; }
        public DateTime MedlemStart { get; set; }
        public DateTime MedlemSlutt { get; set; }
    }
}