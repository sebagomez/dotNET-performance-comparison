using System;
using System.Diagnostics;
using System.Numerics;

namespace StandardLibrary
{
	public class MathTests
    {
		public static void BigIntegerModPow()
		{
			var rand = new Random(42);
			BigInteger a = Create(rand, 8192);
			BigInteger b = Create(rand, 8192);
			BigInteger c = Create(rand, 8192);

			var sw = Stopwatch.StartNew();
			BigInteger.ModPow(a, b, c);
			Console.WriteLine(sw.Elapsed);
		}

		private static BigInteger Create(Random rand, int bits)
		{
			var value = new byte[(bits + 7) / 8 + 1];
			rand.NextBytes(value);
			value[value.Length - 1] = 0;
			return new BigInteger(value);
		}

		public static void MathDivRem()
		{
			long a = 99, b = 10, div, rem;

			var sw = Stopwatch.StartNew();
			for (int i = 0; i < 100_000_000; i++)
			{
				div = Math.DivRem(a, b, out rem);
			}
			Console.WriteLine(sw.Elapsed);
		}

	}
}
