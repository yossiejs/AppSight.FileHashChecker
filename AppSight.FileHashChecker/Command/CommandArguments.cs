using System;
using System.Collections.Generic;
using System.Text;

namespace AppSight.FileHashChecker.Command
{
    public class CommandArguments
    {
        public string ExePath { get; set; }
        public string FilePath { get; set; }
        public CommandOptions Options { get; set; }
    }
}
