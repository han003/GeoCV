using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class MyProjectViewModel
    {
        public IEnumerable<Prosjekt> Prosjekt { get; set; }
        public IEnumerable<ListeKatalog> Stillinger { get; set; }
        public IEnumerable<MyProjectCustomObject> Data { get; set; }
    }
}