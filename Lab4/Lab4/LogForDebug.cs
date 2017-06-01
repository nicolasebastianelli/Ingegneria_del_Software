using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    class LogForDebug : ILogger
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
