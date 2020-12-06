using System;
using AppSight.FileHashChecker.Library.Security;
using Xunit;

namespace AppSight.FileHashChecker.Library.Tests
{
    public class FileHashCalculatorTest
    {
        [Fact]
        public void TestCalculateBinaryFile()
        {
            var calculator = new FileHashCalculator();
            var md5Hash = calculator.Calculate("tux.png", HashType.MD5);
            var sha1Hash = calculator.Calculate("tux.png", HashType.SHA1);
            var sha256Hash = calculator.Calculate("tux.png", HashType.SHA256);

            Assert.Equal("bede490bfe83a2847e1503b3eb5085c7", md5Hash.ComputedHash.ToHashString());
            Assert.Equal("be598b6d3e3f4f4232abbdc1e90ff900a0c3ccd2", sha1Hash.ComputedHash.ToHashString());
            Assert.Equal("8fc6897c39e60c0d246d992c9b7057ac483f37a7b0d85b69bdc42c652e9a60ee", sha256Hash.ComputedHash.ToHashString());
        }

        [Fact]
        public void TestCalculateAsciiFile()
        {
            var calculator = new FileHashCalculator();
            var md5Hash = calculator.Calculate("tux.txt", HashType.MD5);
            var sha1Hash = calculator.Calculate("tux.txt", HashType.SHA1);
            var sha256Hash = calculator.Calculate("tux.txt", HashType.SHA256);

            Assert.Equal("5ab6e4b98bc7fb716fc07ed98fedf802", md5Hash.ComputedHash.ToHashString());
            Assert.Equal("2970eb1fef549b6a4039e7fd2336ba16b18bcbd2", sha1Hash.ComputedHash.ToHashString());
            Assert.Equal("ec2dc675422c8eeb1eef26e7c67c3d74713e947bda7378896d8e3cf4dc5f0161", sha256Hash.ComputedHash.ToHashString());
        }
    }
}
