﻿using System;
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

				CollectionsTests.QueueEnqueueDequeue(); // <-- Start here 2x
				//CollectionsTests.SortedSetEnumerableCtorDuplicate(); // 600x
				//CollectionsTests.SortedSetMin(); // <-- 13x
				//CollectionsTests.ListAddRemoveAt();
				//CollectionsTests.ConcurrentQueueEnqueueTryDequeue(); // <--
				//CollectionsTests.ConcurrentQueueProducerSpinningConsumer();
				//CollectionsTests.ConcurrentQueueEnqueueTryDequeueMemory(); // <-- MEM
				//CollectionsTests.ConcurrentBagAddTryTake();
				//LinQTests.ConcatTiming(); 
				//LinQTests.OredBySkipFirst();
				//LinQTests.SelectToList(); // <--
				//LinQTests.ToArray();
				//CompressionTests.CompressDecompress();
				//CryptographyTests.ComputeHash();
				//MathTests.BigIntegerModPow(); // <-- 21sec Framework
				//MathTests.MathDivRem(); // <--
				//SerializationTests.BinaryFormatterDeserialize(); // <-- 18 vs 114
				//TextProcessingTests.RegexIsMatch();
				//TextProcessingTests.WebUtilityUrlDecode();
				//TextProcessingTests.EncodingUTF8GetBytes();
				//TextProcessingTests.EnumParse();
				//TextProcessingTests.DateTimeToString(); // <-- 3x 
				//TextProcessingTests.StringIndexOfChar();
				//TextProcessingTests.StringStartsWithOrdinal();
				//FileSystemTests.FileStreamAsyncReadWrite().GetAwaiter().GetResult();
				//NetworkingTests.NetworkingSynchronousCompletitionSendReceive();
				//NetworkingTests.NetworkStreamWriteAsyncCopyToAsync().GetAwaiter().GetResult();
				//NetworkingTests.SslStreamNetworkStream().GetAwaiter().GetResult(); //missing file
				//ConcurrencyTests.ThreadPoolQueuePoolWorkITemProcessorCount();
				//ConcurrencyTests.SpinLockTryEnterAlreadyAcquired(); // <-- 6x
				//ConcurrencyTests.LazyValue();

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
