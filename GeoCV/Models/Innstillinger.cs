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
    
    public partial class Innstillinger
    {
        public Innstillinger()
        {
            this.CVVersjon = new HashSet<CVVersjon>();
        }
    
        public int InnstillingerId { get; set; }
        public bool Fornavn { get; set; }
        public bool Mellomnavn { get; set; }
        public bool Etternavn { get; set; }
        public bool Stilling { get; set; }
        public bool ÅrErfaring { get; set; }
        public bool Språk { get; set; }
        public bool Nasjonalitet { get; set; }
        public bool Fødselsår { get; set; }
        public bool Programmeringsspråk { get; set; }
        public bool Rammeverk { get; set; }
        public bool WebTeknologier { get; set; }
        public bool Databasesystemer { get; set; }
        public bool Serverside { get; set; }
        public bool Operativsystemer { get; set; }
        public bool Annet { get; set; }
        public bool Utdannelse { get; set; }
        public bool Arbeidserfaring { get; set; }
        public bool Prosjekter { get; set; }
    
        public virtual ICollection<CVVersjon> CVVersjon { get; set; }
    }
}
