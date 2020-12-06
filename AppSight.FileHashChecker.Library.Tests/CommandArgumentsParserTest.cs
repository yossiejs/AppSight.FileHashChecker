using System;
using AppSight.FileHashChecker.Library.Command;
using AppSight.FileHashChecker.Library.Security;
using Xunit;

namespace AppSight.FileHashChecker.Library.Tests
{
    public class CommandArgumentsParserTest
    {
        [Fact]
        public void TestParse()
        {
            var parser = new CommandArgumentsParser();
            var args = new[] { "C:\\path\\to\\AppSight.FileHashChecker.exe", "-h", "-f", "C:\\path\\to\\note.txt", "-t", "sha1" };
            var commandArguments = parser.Parse(args);

            Assert.Equal("C:\\path\\to\\AppSight.FileHashChecker.exe", commandArguments.ExePath);
            Assert.Equal("C:\\path\\to\\note.txt", commandArguments.Options.FilePath);
            Assert.Equal(HashType.SHA1, commandArguments.Options.HashType);
            Assert.True(commandArguments.Options.Help);
        }
    }
}
