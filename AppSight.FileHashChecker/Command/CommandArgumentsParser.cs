using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppSight.FileHashChecker.Command
{
    public class CommandArgumentsParser
    {
        public CommandArguments Parse(string[] args)
        {
            var commandArguments = new CommandArguments();

            foreach (var (arg, index) in args.Select((arg, index) => (arg, index)))
            {
                if (index == 0)
                {
                    commandArguments.ExePath = arg;
                }
                else if (index == 1)
                {
                    commandArguments.FilePath = arg;
                }
                else
                {

                }
            }

            return commandArguments;
        }

        private CommandArguments ParseOption(string arg)
        {
            
        }
    }
}
