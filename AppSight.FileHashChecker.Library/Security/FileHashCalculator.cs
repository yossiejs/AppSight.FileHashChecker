using System;
using System.IO;
using System.Security.Cryptography;

namespace AppSight.FileHashChecker.Library.Security
{
    public class FileHashCalculator
    {
        public FileHash Calculate(string filePath, HashType hashType)
        {
            if (string.IsNullOrEmpty(filePath)) { throw new ArgumentException(nameof(filePath)); }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                switch (hashType)
                {
                    case HashType.MD5:
                        {
                            var hashBytes = MD5.Create().ComputeHash(fileStream);
                            return new FileHash
                            {
                                HashType = hashType,
                                ComputedHash = hashBytes,
                                Path = filePath,
                            };
                        }

                    case HashType.SHA1:
                        {
                            var hashBytes = SHA1.Create().ComputeHash(fileStream);
                            return new FileHash
                            {
                                HashType = hashType,
                                ComputedHash = hashBytes,
                                Path = filePath,
                            };
                        }

                    case HashType.SHA256:
                        {
                            var hashBytes = SHA256.Create().ComputeHash(fileStream);
                            return new FileHash
                            {
                                HashType = hashType,
                                ComputedHash = hashBytes,
                                Path = filePath,
                            };
                        }

                    default:
                        throw new InvalidOperationException($"Specified unknown hash type. hashType={hashType}");
                }
            }
        }
    }
}