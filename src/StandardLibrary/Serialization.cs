using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StandardLibrary
{
	public class SerializationTests
    {
		[Serializable]
		private class Book
		{
			public string Name;
			public string Id;
		}


		public static void BinaryFormatterDeserialize()
		{
			var books = new List<Book>();
			for (int i = 0; i < 1_000_000; i++)
			{
				string id = i.ToString();
				books.Add(new Book { Name = id, Id = id });
			}

			var formatter = new BinaryFormatter();
			var mem = new MemoryStream();
			formatter.Serialize(mem, books);
			mem.Position = 0;

			var sw = Stopwatch.StartNew();
			formatter.Deserialize(mem);
			sw.Stop();

			Console.WriteLine(sw.Elapsed.TotalSeconds);
		}
	}
}
