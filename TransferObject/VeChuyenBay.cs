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
    
    public partial class VeChuyenBay
    {
        public int maVe { get; set; }
        public string tenHK { get; set; }
        public Nullable<int> maHD { get; set; }
        public Nullable<int> maGhe { get; set; }
        public Nullable<int> maCB { get; set; }
    
        public virtual Ghe_ChuyenBay Ghe_ChuyenBay { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
