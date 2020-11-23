using CongVanManager.Command;
using CongVanManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

public enum DocType { In, Out };

namespace CongVanManager.ViewModel
{
    class NewDocLayoutViewModel
    {
        public DocType NewDocLayout_Type;
        public int iNewDocLayout_Type { get; set; }

        public NewDocLayoutViewModel(DocType type)
        {
            NewDocLayout_Type = type;
            iNewDocLayout_Type = (int)NewDocLayout_Type;
        }
    }
}
