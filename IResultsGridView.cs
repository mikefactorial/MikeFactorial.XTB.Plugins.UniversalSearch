using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    interface IResultsGridView
    {
        List<DataGridViewCell> HilightedCells { get; }
    }
}
