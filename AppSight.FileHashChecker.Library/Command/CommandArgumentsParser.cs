﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppSight.FileHashChecker.Library.Security;

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
                    "AppSight.FileHashChecker.exe -f C:\\path\\to\\notepad.exe -t md5");
            }

            var commandArguments = new CommandArguments
            {
                Options = new CommandOptions(),
            };

            string currentOptionName = String.Empty;

            foreach (var (arg, index) in args.Select((arg, index) => (arg, index)))
            {
                if (index == 0)
                {
                    commandArguments.ExePath = arg;
                    continue;
                }

                if (string.IsNullOrEmpty(currentOptionName))
                {
                    currentOptionName = arg;

                    switch (currentOptionName)
                    {
                        case CommandArgumentsOptionNames.FilePath:
                        case CommandArgumentsOptionNames.FilePathShort:
                        case CommandArgumentsOptionNames.HashType:
                        case CommandArgumentsOptionNames.HashTypeShort:
                            break;

                        default:
                            throw new ArgumentException($"Unknown option name. optionName={currentOptionName}");
                    }
                }
                else
                {
                    var optionValue = arg;

                    switch (currentOptionName)
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
                                throw new ArgumentException($"Invalid option value. optionName={currentOptionName}, optionValue={optionValue}");
                            }

                            break;
                    }

                    currentOptionName = String.Empty;
                }
            }

            return commandArguments;
        }
    }
}
