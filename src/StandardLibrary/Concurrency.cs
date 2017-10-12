using System;
using System.Diagnostics;
using System.Threading;

namespace StandardLibrary
{
	public class Concurrency
	{
		public static void ThreadPoolProcessorCount()
		{
			while (true)
			{
				int remaining = 20_000_000;
				var mres = new ManualResetEventSlim();
				WaitCallback wc = null;
				wc = delegate
				{
					if (Interlocked.Decrement(ref remaining) <= 0)
						mres.Set();
					else
						ThreadPool.QueueUserWorkItem(wc);
				};

				var sw = new Stopwatch();
				int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
				sw.Start();

				for (int i = 0; i < Environment.ProcessorCount; i++) ThreadPool.QueueUserWorkItem(wc);
				mres.Wait();

				Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
			}

		}

		public static void SpinLock()
		{
			while (true)
			{
				bool taken = false;
				var sl = new SpinLock(false);
				sl.Enter(ref taken);

				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 100_000_000; i++)
				{
					taken = false;
					sl.TryEnter(0, ref taken);
				}
				Console.WriteLine(sw.Elapsed);
			}
		}

		public static void LazyValues()
		{
			int result;
			while (true)
			{
				var lazy = new Lazy<int>(() => 42);
				result = lazy.Value;

				var sw = Stopwatch.StartNew();
				for (int i = 0; i < 1_000_000_000; i++)
				{
					result = lazy.Value;
				}

				Console.WriteLine(sw.Elapsed);
			}

		}
	}
}
