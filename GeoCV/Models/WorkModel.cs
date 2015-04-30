using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoCV.Models
{
    public class WorkModel
    {
        public IEnumerable<ListeKatalog> Stillinger { get; set; }
        public CVVersjon BrukerCv { get; set; }
    }
}