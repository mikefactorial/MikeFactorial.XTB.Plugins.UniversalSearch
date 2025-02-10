using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    public class ResultsDataGridView : DataGridView, IResultsGridView
    {
        List<DataGridViewCell> hilightedCells = new List<DataGridViewCell>();
        public List<DataGridViewCell> HilightedCells
        {
            get
            {
                return hilightedCells;
            }
        }
    }
}
