using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager.ViewModel
{
    abstract class FilterSetting : ObservableObject
    {
        public abstract bool Filter(CongVan cv);
    }
}
