using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemarkedUp
{
    public class GenerateSpecification
    {
        public IEnumerable<String> FileFilter { get; set; }
        public int ColumnWidth { get; set; }
    }
}
