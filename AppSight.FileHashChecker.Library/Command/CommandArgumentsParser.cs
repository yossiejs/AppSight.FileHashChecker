using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppSight.FileHashChecker.Security;

namespace AppSight.FileHashChecker.Library.Command
{
    public class CommandArgumentsParser
    {
        public CommandArguments Parse(string[] args)
        {
            if (args.Length <= 1)
            {
                throw new ArgumentException(
                    "Required options are not specified.\r\n" +
                    "\r\n" +
                    "e.g.\r\n" +
                    "AppSight.FileHashChecker.exe C:\\WINDOWS\\system32\\notepad.exe -a=md5");
            }

            var commandArguments = new CommandArguments
            {
                Options = new CommandOptions(),
            };

            foreach (var (arg, index) in args.Select((arg, index) => (arg, index)))
            {
                if (index == 0)
                {
                    commandArguments.ExePath = arg;
                }
                else
                {
                    var option = arg.Split("=");

                    if (option.Length <= 1)
                    {
                        throw new ArgumentException($"Invalid arguments format. arg={arg}");
                    }

                    var optionName = option[0];
                    var optionValue = option[1];

                    switch (optionName)
                    {
                        case CommandArgumentsOptionNames.FilePath:
                        case CommandArgumentsOptionNames.FilePathShort:
                            commandArguments.Options.FilePath = optionValue;
                            break;

                        case CommandArgumentsOptionNames.HashType:
                        case CommandArgumentsOptionNames.HashTypeShort:
                            HashType hashType;

                            if (Enum.TryParse(optionValue, true, out hashType))
                            {
                                commandArguments.Options.HashType = hashType;
                            }
                            else
                            {
                                throw new ArgumentException($"Invalid option value. optionName={optionName}, optionValue={optionValue}");
                            }
                            
                            break;

                        default:
                            throw new ArgumentException($"Unknown option name. optionName={optionName}");
                    }
                }
            }

            return commandArguments;
        }
    }
}
