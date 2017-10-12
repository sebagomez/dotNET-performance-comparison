using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace StandardLibrary
{
	public class FileSystem
    {
		public static async Task FileStreamAsyncReadWrite()
		{
			string inputPath = Path.GetTempFileName(), outputPath = Path.GetTempFileName();
			byte[] data = new byte[50_000_000];
			new Random().NextBytes(data);
			File.WriteAllBytes(inputPath, data);

			var sw = new Stopwatch();
			int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
			sw.Start();

			for (int i = 0; i < 100; i++)
			{
				using (var input = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 0x1000, useAsync: true))
				using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 0x1000, useAsync: true))
				{
					await input.CopyToAsync(output);
				}
			}

			Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
		}
	}
}
