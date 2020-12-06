using AppSight.FileHashChecker.Security;

namespace AppSight.FileHashChecker.Library.Command
{
    public class CommandOptions
    {
        public string FilePath { get; set; }
        public HashType HashType { get; set; }
    }
}