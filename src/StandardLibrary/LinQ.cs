using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace StandardLibrary
{
	public class LinQTests
    {
		public static void ConcatTiming()
		{
			IEnumerable<int> zeroToTen = Enumerable.Range(0, 10);
			IEnumerable<int> result = zeroToTen;

			for (int i = 0; i < 10_000; i++)
			{
				result = result.Concat(zeroToTen);
			}

			var sw = Stopwatch.StartNew();

			foreach (int i in result) { }

			Console.WriteLine(sw.Elapsed);
		}

		public static void OredBySkipFirst()
		{
			IEnumerable<int> tenMillionToZero = Enumerable.Range(0, 10_000_000).Reverse();
			while (true)
			{
				var sw = Stopwatch.StartNew();
				int fifth = tenMillionToZero.OrderBy(i => i).Skip(4).First();
				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void SelectToList()
		{
			IEnumerable<int> zeroToTenMillion = Enumerable.Range(0, 10_000_000).ToArray();
			while (true)
			{
				var sw = Stopwatch.StartNew();
				zeroToTenMillion.Select(i => i).ToList();
				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void ToArray()
		{
			IEnumerable<int> source = Enumerable.Range(0, 100_000_000);
			while (true)
			{
				var sw = new Stopwatch();
				int gen0 = GC.CollectionCount(0);
				int gen1 = GC.CollectionCount(1);
				int gen2 = GC.CollectionCount(2);
				sw.Start();

				source.ToArray();

				Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
			}
		}
    }
}
