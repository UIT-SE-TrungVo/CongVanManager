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
    using System.Collections.ObjectModel;

    public partial class NoiNhan
    {
        public virtual CongVan CongVan { get; set; }
        public virtual LienHe LienHe { get; set; }

        private static ObservableCollection<NoiNhan> _db;
        public static ObservableCollection<NoiNhan> DB
        {
            get
            {
                if (_db == null)
                    _db = new ObservableCollection<NoiNhan>();
                return _db;
            }
            private set { }
        }
    }
}
