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
    using System.Collections.Specialized;
    using System.Linq;
    using CongVanManager.View;
    using CongVanManager.ViewModel;

    public partial class LienHe : ObservableObject, IComparable
    {
        public LienHe()
        {
            this.CongVans = new DelayedObservableCollection<CongVan>();
            //this.CongVans.CollectionChanged += (obj, arg) => OnPropertyChanged("CongVans");
            this.DanhSachNoiNhan = new DelayedObservableCollection<NoiNhan>();
            //this.DanhSachNoiNhan.CollectionChanged += (obj, arg) => OnPropertyChanged("DanhSachNoiNhan");
        }

        public LienHe(View.LienHe lh)
        {
            Email = lh.Email;
            Name = lh.TenLienHe;
            this.CongVans = new DelayedObservableCollection<CongVan>();
            //this.CongVans.CollectionChanged += (obj, arg) => OnPropertyChanged("CongVans");
            this.DanhSachNoiNhan = new DelayedObservableCollection<NoiNhan>();
            //this.DanhSachNoiNhan.CollectionChanged += (obj, arg) => OnPropertyChanged("DanhSachNoiNhan");
        }
    
        public string Email { get; set; }
        public string Name { get; set; }
    
        public virtual DelayedObservableCollection<CongVan> CongVans { get; set; }
        public virtual DelayedObservableCollection<NoiNhan> DanhSachNoiNhan { get; set; }

        public static void ReloadDatabase()
        {
            _db.CollectionChanged -= LienHeDBChanged;

            LienHe.DB.Clear();
            foreach (View.LienHe cv in DataProvider.Ins.DB.LienHe)
                _db.Add(new LienHe(cv));

            _db.CollectionChanged += LienHeDBChanged;
        }
        static void LienHeDBChanged(object sender, NotifyCollectionChangedEventArgs arg)
        {
            if (arg.Action == NotifyCollectionChangedAction.Move)
                return;
            if (arg.OldItems != null)
                foreach (LienHe item in arg.OldItems)
                {
                }
            if (arg.NewItems != null)
                foreach (LienHe item in arg.NewItems)
                {
                    bool newItem = false;
                    var temp = DataProvider.Ins.DB.LienHe.Find(item.Email);

                    if (temp == null)
                    {
                        newItem = true;
                        temp = item.ToLienHe();
                    }

                        if (temp.CongVans1 == null)
                            temp.CongVans1 = new List<View.CongVan>();

                    foreach (NoiNhan nh in item.DanhSachNoiNhan)
                    {
                        nh.LienHe1 = temp;
                        if (nh.CongVan1 != null)
                        {
                            temp.CongVans1.Add(nh.CongVan1);
                            nh.CongVan1.LienHes.Add(temp);
                        }
                    }
                    if (newItem)
                        DataProvider.Ins.DB.LienHe.Add(temp);
                }
        }

        private static DelayedObservableCollection<LienHe> _db;
        public static DelayedObservableCollection<LienHe> DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new DelayedObservableCollection<LienHe>();
                    ReloadDatabase();
                }
                return _db;
            }
            private set { }
        }

        private View.LienHe ToLienHe()
        {
            return new View.LienHe
            {
                Email = Email,
                TenLienHe = Name
            };
        }

        public static LienHe Get(string Email)
        {
            foreach (LienHe item in DB)
            {
                if (item.Email == Email)
                    return item;
            }
            return null;
        }

        public int CompareTo(object obj)
        {
            return Email.CompareTo((obj as LienHe).Email);
        }
    }
}
