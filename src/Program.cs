using System;
using System.Diagnostics;
using StandardLibrary;

namespace ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				//For a detailed explanation of each case take a look at https://blogs.msdn.microsoft.com/dotnet/2017/06/07/performance-improvements-in-net-core/

				//CollectionsTests.Queue();
				//CollectionsTests.SortedSet();
				//CollectionsTests.SortedSet2();
				//CollectionsTests.List();
				//CollectionsTests.ConcurrentQueue();
				//CollectionsTests.ConcurrentQueue2();
				//CollectionsTests.ConcurrentQueue2GC();
				//CollectionsTests.ConcurrentBag();
				LinQ.Concat(); // <--
				//LinQ.OredBySkip(); // <--
				//LinQ.SelectToList();
				//LinQ.ToArray();
				//Compression.CompressDecompress();
				//Cryptography.ComputeHash();
				//StandardLibrary.Math.BigIntegerModPow(); // <--
				//StandardLibrary.Math.DivRem();
				//Serialization.BinaryFormatterDeserialize();
				//TextProcessing.RegexIsMatch();
				//TextProcessing.UrlDecode();
				//TextProcessing.UTF8EncodingGetBytes();
				//TextProcessing.EnumParse();
				//TextProcessing.DateTimeToString();
				//TextProcessing.StringIndexOf();
				//TextProcessing.StringStartsWith();
				//FileSystem.FileStreamAsyncReadWrite().GetAwaiter().GetResult();
				//Networking.SocketSendReceive();
				//Networking.StreamWriteCopyTo().GetAwaiter().GetResult();
				//Networking.SslNetworkStream().GetAwaiter().GetResult(); //missing file
				//Concurrency.ThreadPoolProcessorCount();
				//Concurrency.SpinLock(); // <--
				//Concurrency.LazyValues(); // <--

			}
			catch (Exception ex)
			{
				Console.WriteLine($"ERROR:{ex.Message}");
			}
			finally
			{
				if (Debugger.IsAttached)
				{
					Console.WriteLine("Press <enter> to quit...");
					Console.ReadLine();
				}
			}
		}
}
}
