//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeoCV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kompetanse
    {
        public Kompetanse()
        {
            this.CVVersjon = new HashSet<CVVersjon>();
        }
    
        public int KompetanseId { get; set; }
        public string Programmeringsspråk { get; set; }
        public string Rammeverk { get; set; }
        public string WebTeknologier { get; set; }
        public string Databasesystemer { get; set; }
        public string Serverside { get; set; }
        public string Operativsystemer { get; set; }
    
        public virtual ICollection<CVVersjon> CVVersjon { get; set; }
    }
}
