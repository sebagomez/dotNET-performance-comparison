using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace StandardLibrary
{
	public class Cryptography
    {
		public static void ComputeHash()
		{
			byte[] raw = new byte[100 * 1024 * 1024];
			for (int i = 0; i < raw.Length; i++)
				raw[i] = (byte)i;

			using (var sha = SHA256.Create())
			{
				var sw = Stopwatch.StartNew();
				sha.ComputeHash(raw);
				Console.WriteLine(sw.Elapsed);
			}
		}
    }
}
