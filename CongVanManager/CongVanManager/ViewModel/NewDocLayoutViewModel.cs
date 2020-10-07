using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

public enum DocType { In, Out };

namespace CongVanManager.ViewModel
{
    class NewDocLayoutViewModel
    {
        public DocType NewDocLayout_Type;

        public NewDocLayoutViewModel(DocType type)
        {
            NewDocLayout_Type = type;
        }
    }
}
