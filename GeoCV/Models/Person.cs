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
    
    public partial class Person
    {
        public Person()
        {
            this.CVVersjon = new HashSet<CVVersjon>();
            this.Medlem = new HashSet<Medlem>();
        }
    
        public int PersonId { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Stilling { get; set; }
        public Nullable<short> ÅrErfaring { get; set; }
        public string Språk { get; set; }
        public System.DateTime LagtTil { get; set; }
        public System.DateTime Modifisert { get; set; }
    
        public virtual ICollection<CVVersjon> CVVersjon { get; set; }
        public virtual ICollection<Medlem> Medlem { get; set; }
    }
}
