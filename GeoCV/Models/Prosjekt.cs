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
    
    public partial class Prosjekt
    {
        public Prosjekt()
        {
            this.Medlem = new HashSet<Medlem>();
            this.TekniskProfil = new HashSet<TekniskProfil>();
        }
    
        public int ProsjektId { get; set; }
        public string Navn { get; set; }
        public string Kunde { get; set; }
        public string Beskrivelse { get; set; }
        public Nullable<short> Fra { get; set; }
        public Nullable<short> Til { get; set; }
        public bool Avsluttet { get; set; }
    
        public virtual ICollection<Medlem> Medlem { get; set; }
        public virtual ICollection<TekniskProfil> TekniskProfil { get; set; }
    }
}
