//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CongVanManager
{
    using System;
    using System.Collections.Generic;
    
    public partial class HopCongVan
    {
        public HopCongVan()
        {
            this.CongVans = new HashSet<CongVan>();
        }
    
        public int LoaiHop { get; set; }
    
        public virtual ICollection<CongVan> CongVans { get; set; }
    }
}