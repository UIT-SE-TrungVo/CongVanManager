using CongVanManager.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CongVanManager.ViewModel
{
    class UC_InBoxFilterViewModel : FilterSetting
    {
        #region FilterSetting
        private List<Func<CongVan, bool>> filterList = new List<Func<CongVan, bool>>(4);
        private Func<CongVan, bool> defaultFilter = (item) => false;
        public override bool Filter(CongVan item)
        {
            string filterText = MainWindowViewModel.Ins.FilterText;

            if (!Match(item, filterText))
                return false;

            foreach (var func in filterList)
                if (func?.Invoke(item) == true)
                    return true;
            return false;
        }
        private bool _chuaDoc;
        public bool ChuaDoc
        {
            get => _chuaDoc;
            set
            {
                _chuaDoc = value;
                if (value == false)
                    filterList[0] = defaultFilter;
                else
                    filterList[0] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.ChuaDoc;
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
                        item.StatusCode == CongVan.StatusCodeEnum.DaDuyet_Den;
                OnPropertyChanged();
            }
        }
        private bool _daTiepNhan;
        public bool DaTiepNhan
        {
            get => _daTiepNhan;
            set
            {
                _daTiepNhan = value;
                if (value == false)
                    filterList[2] = defaultFilter;
                else
                    filterList[2] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.DaTiepNhan;
                OnPropertyChanged();
            }
        }

        private UC_InBoxFilterViewModel()
        {
            while (filterList.Count < filterList.Capacity)
                filterList.Add(defaultFilter);
            DaChuyen = true;
            DaDuyet = true;
            ChuaDoc = true;
            DaTiepNhan = true;
        }
        private static UC_InBoxFilterViewModel ins = null;
        public static UC_InBoxFilterViewModel Ins
        {
            get
            {
                if (ins == null)
                    ins = new UC_InBoxFilterViewModel();
                return ins;
            }
            private set => ins = value;
        }

        private bool _daChuyen;
        public bool DaChuyen
        {
            get => _daChuyen;
            set
            {
                _daChuyen = value;
                if (value == false)
                    filterList[3] = defaultFilter;
                else
                    filterList[3] = (item) =>
                        item.StatusCode == CongVan.StatusCodeEnum.DaChuyen;
                OnPropertyChanged();
            }
        }
        #endregion
        
        #region ButtonFilter
        private ICommand _buttonFilterCongVan;
        public ICommand ButtonFilterCongVan
        {
            get
            {
                if (_buttonFilterCongVan == null)
                    _buttonFilterCongVan = new RelayCommand(param =>
                    {
                        MainWindowViewModel.Ins.ButtonFilterCongVan.Execute(null);
                    });
                return _buttonFilterCongVan;
            }
        }
        #endregion
    }
}
