using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikeFactorial.XTB.Plugins.UniversalSearch
{
    public class SolutionSearchResult
    {
        public string FileLink { get; set; }
        public int LineNumber { get; set; }
        public string FilePath { get; set; }
        public string Value { get; set; }
    }
}
