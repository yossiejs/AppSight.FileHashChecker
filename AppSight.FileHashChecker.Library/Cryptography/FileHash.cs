using System;
using System.Collections.Generic;
using System.Text;

namespace AppSight.FileHashChecker.Library.Cryptography
{
    public class FileHash
    {
        public HashType HashType { get; set; }
        public byte[] ComputedHash { get; set; }
        public string Path { get; set; }
    }
}
