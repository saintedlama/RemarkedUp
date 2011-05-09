using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RemarkedUp
{
    public class CleanCommand
    {
        public void Execute()
        {
            // TODO: Some exception handling missing
            Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "style"), true);
        }
    }
}
