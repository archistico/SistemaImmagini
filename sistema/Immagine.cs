using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistema
{
    public class Immagine
    {
        public string source;
        public string target;
        public string targetPath;

        public Immagine(string Source, string Target, string TargetPath)
        {
            source = Source;
            target = Target;
            targetPath = TargetPath;
        }
    }
}
