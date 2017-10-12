using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace StandardLibrary
{
	public class TextProcessing
    {
		public static void RegexIsMatch()
		{
			var sw = new Stopwatch();
			int gen0 = GC.CollectionCount(0);
			sw.Start();

			for (int i = 0; i < 10_000_000; i++)
			{
				Regex.IsMatch("555-867-5309", @"^\d{3}-\d{3}-\d{4}$");
			}

			Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0}");

		}

		public static void UrlDecode()
		{
			var sw = new Stopwatch();
			int gen0 = GC.CollectionCount(0);
			sw.Start();

			for (int i = 0; i < 10_000_000; i++)
			{
				WebUtility.UrlDecode("abcdefghijklmnopqrstuvwxyz");
			}

			Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0}");

		}

		public static void UTF8EncodingGetBytes()
		{
			string s = new string(Enumerable.Range(0, 1024).Select(i => (char)('a' + i)).ToArray());
			while (true)
			{
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 1_000_000; i++)
				{
					byte[] data = Encoding.UTF8.GetBytes(s);
				}
				Console.WriteLine(sw.Elapsed);
			}
		}

		[Flags]
		private enum Colors
		{
			Red = 0x1,
			Orange = 0x2,
			Yellow = 0x4,
			Green = 0x8,
			Blue = 0x10
		}

		public static void EnumParse()
		{
			while (true)
			{
				var sw = new Stopwatch();
				int gen0 = GC.CollectionCount(0);
				sw.Start();

				for (int i = 0; i < 2_000_000; i++)
				{
					Enum.Parse(typeof(Colors), "Red, Orange, Yellow, Green, Blue");
				}

				Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0}");
			}
		}

		public static void DateTimeToString()
		{
			var dt = DateTime.Now;
			while (true)
			{
				var sw = new Stopwatch();
				int gen0 = GC.CollectionCount(0);
				sw.Start();

				for (int i = 0; i < 2_000_000; i++)
				{
					dt.ToString("o");
					dt.ToString("r");
				}

				Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0}");
			}

		}

		public static void StringIndexOf()
		{
			string s = string.Concat(Enumerable.Repeat("a", 100)) + "b";
			while (true)
			{
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 100_000_000; i++)
				{
					s.IndexOf('b');
				}

				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void StringStartsWith()
		{
			string s = "abcdefghijklmnopqrstuvwxyz";
			while (true)
			{
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 100_000_000; i++)
				{
					s.StartsWith("abcdefghijklmnopqrstuvwxy-", StringComparison.Ordinal);
				}

				Console.WriteLine(sw.Elapsed);
			}
		}
	}
}
