//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransferObject
{
    using System;
    using System.Collections.Generic;
    
    public partial class MayBay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MayBay()
        {
            this.Ghes = new HashSet<Ghe>();
        }
    
        public int maMB { get; set; }
        public string tenMB { get; set; }
        public Nullable<int> slGheH1 { get; set; }
        public Nullable<int> slGhePT { get; set; }
        public Nullable<int> maLoaiMB { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ghe> Ghes { get; set; }
        public virtual LoaiMayBay LoaiMayBay { get; set; }
    }
}
