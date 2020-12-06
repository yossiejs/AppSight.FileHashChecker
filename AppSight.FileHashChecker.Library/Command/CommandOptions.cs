using AppSight.FileHashChecker.Library.Cryptography;

namespace AppSight.FileHashChecker.Library.Command
{
    public class CommandOptions
    {
        public string FilePath { get; set; }
        public HashType HashType { get; set; }
        public bool Help { get; set; }
    }
}