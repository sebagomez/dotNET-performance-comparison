using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StandardLibrary
{
	public  class CollectionsTests
    {
		public static void Queue()
		{
			while (true)
			{

				var q = new Queue<int>();
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 100_000_000; i++)
				{
					q.Enqueue(i);
					q.Dequeue();
				}

				Console.WriteLine(sw.Elapsed);

			}
		}

		public static void SortedSet()
		{
			var sw = Stopwatch.StartNew();
			var ss = new SortedSet<int>(Enumerable.Repeat(42, 400_000));
			Console.WriteLine(sw.Elapsed);
		}

		public static void SortedSet2()
		{
			int result;
			while (true)
			{
				var s = new SortedSet<int>();
				for (int n = 0; n < 100_000; n++)
				{
					s.Add(n);
				}

				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 10_000_000; i++)
				{
					result = s.Min;
				}

				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void List()
		{
			while (true)
			{
				var l = new List<int>();
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 100_000_000; i++)
				{
					l.Add(i);
					l.RemoveAt(0);
				}

				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void ConcurrentQueue()
		{
			while (true)
			{
				var q = new ConcurrentQueue<int>();
				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 100_000_000; i++)
				{
					q.Enqueue(i);
					q.TryDequeue(out int _);
				}

				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void ConcurrentQueue2()
		{
			while (true)
			{
				const int Items = 100_000_000;
				var q = new ConcurrentQueue<int>();
				var sw = Stopwatch.StartNew();

				Task consumer = Task.Run(() =>
				{
					int total = 0;
					while (total < Items) if (q.TryDequeue(out int _)) total++;
				});

				for (int i = 0; i < Items; i++)
					q.Enqueue(i);

				consumer.Wait();

				Console.WriteLine(sw.Elapsed);

			}
		}

		public static void ConcurrentQueue2GC()
		{
			while (true)
			{
				var q = new ConcurrentQueue<int>();
				int gen0 = GC.CollectionCount(0);
				int gen1 = GC.CollectionCount(1);
				int gen2 = GC.CollectionCount(2);

				for (int i = 0; i < 100_000_000; i++)
				{
					q.Enqueue(i);
					q.TryDequeue(out int _);
				}

				Console.WriteLine($"Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
			}
		}

		public static void ConcurrentBag()
		{
			while (true)
			{
				var q = new ConcurrentBag<int>() { 1, 2 };
				var sw = new Stopwatch();

				int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
				sw.Start();

				for (int i = 0; i < 100_000_000; i++)
				{
					q.Add(i);
					q.TryTake(out int _);
				}

				sw.Stop();

				Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
			}
		}
    }
}
