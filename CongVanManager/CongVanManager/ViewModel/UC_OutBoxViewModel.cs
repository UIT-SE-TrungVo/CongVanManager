using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager.ViewModel
{
    class UC_OutBoxFilterViewModel : FilterSetting
    {
        private List<Func<CongVan, bool>> filterList = new List<Func<CongVan, bool>>(4);
        private Func<CongVan, bool> defaultFilter = (item) => false;
        public override bool Filter(CongVan item)
        {
            foreach (var func in filterList)
                if (func?.Invoke(item) == true)
                    return true;
            return false;
        }
        private bool _choDuyet;
        public bool ChoDuyet
        {
            get => _choDuyet;
            set
            {
                _choDuyet = value;
                if (value == false)
                    filterList[0] = defaultFilter;
                else
                    filterList[0] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.ChoDuyet;
                OnPropertyChanged();
            }
        }
        private bool _daDuyet;
        public bool DaDuyet
        {
            get => _daDuyet;
            set
            {
                _daDuyet = value;
                if (value == false)
                    filterList[1] = defaultFilter;
                else
                    filterList[1] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.DaDuyet_Di;
                OnPropertyChanged();
            }
        }
        private bool _daLuuTru;
        public bool DaLuuTru
        {
            get => _daLuuTru;
            set
            {
                _daLuuTru = value;
                if (value == false)
                    filterList[2] = defaultFilter;
                else
                    filterList[2] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.DaLuuTru;
                OnPropertyChanged();
            }
        }

        private UC_OutBoxFilterViewModel()
        {
            while (filterList.Count < filterList.Capacity)
                filterList.Add(defaultFilter);

            DaLuuTru = true;
            DaDuyet = true;
            DaGui = true;
            ChoDuyet = true;
        }
        private static UC_OutBoxFilterViewModel ins = null;
        public static UC_OutBoxFilterViewModel Ins
        {
            get
            {
                if (ins == null)
                    ins = new UC_OutBoxFilterViewModel();
                return ins;
            }
            private set => ins = value;
        }

        private bool _daGui;
        public bool DaGui
        {
            get => _daGui;
            set
            {
                _daGui = value;
                if (value == false)
                    filterList[3] = defaultFilter;
                else
                    filterList[3] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.DaGui;
                OnPropertyChanged();
            }
        }
    }
}
