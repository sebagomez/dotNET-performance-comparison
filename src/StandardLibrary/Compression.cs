using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace StandardLibrary
{
	public class CompressionTests
    {
		public static void CompressDecompress()
		{
			// Create some fairly compressible data
			byte[] raw = new byte[100 * 1024 * 1024];
			for (int i = 0; i < raw.Length; i++)
				raw[i] = (byte)i;

			var sw = Stopwatch.StartNew();

			// Compress it
			var compressed = new MemoryStream();
			using (DeflateStream ds = new DeflateStream(compressed, CompressionMode.Compress, true))
			{
				ds.Write(raw, 0, raw.Length);
			}

			compressed.Position = 0;
			
			// Decompress it
			var decompressed = new MemoryStream();
			using (DeflateStream ds = new DeflateStream(compressed, CompressionMode.Decompress))
			{
				ds.CopyTo(decompressed);
			}

			decompressed.Position = 0;

			Console.WriteLine(sw.Elapsed);
		}
    }
}
