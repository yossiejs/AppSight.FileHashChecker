namespace AppSight.FileHashChecker.Library.Security
{
    public class FileHashCalculator
    {
        public FileHash Calculate(string filePath, HashType hashType)
        {

            return new FileHash
            {
                HashType = hashType,
                ComputedHash = null,
                Path = filePath,
            };
        }
    }
}