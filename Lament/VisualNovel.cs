using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lament
{
    internal class VisualNovel
    {
        public static void BeginAt(int lineNumber)
        {
            Dialogue.DisplayLine(lineNumber);
        }
    }
}
