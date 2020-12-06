using System;

namespace AppSight.FileHashChecker.Library.Security
{
    public static class BytesExtensions
    {
		public static string ToHashString(this byte[] bytes)
		{
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}
	}
}
