using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StandardLibrary
{
	public class Networking
	{
		public static void SocketSendReceive()
		{
			using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				listener.Bind(new IPEndPoint(IPAddress.Loopback, 0));
				listener.Listen(1);

				Task connectTask = Task.Run(() => client.Connect(listener.LocalEndPoint));
				using (Socket server = listener.Accept())
				{
					connectTask.Wait();

					using (var clientAre = new AutoResetEvent(false))
					using (var clientSaea = new SocketAsyncEventArgs())
					using (var serverAre = new AutoResetEvent(false))
					using (var serverSaea = new SocketAsyncEventArgs())
					{
						byte[] sendBuffer = new byte[1000];
						clientSaea.SetBuffer(sendBuffer, 0, sendBuffer.Length);
						clientSaea.Completed += delegate { clientAre.Set(); };

						byte[] receiveBuffer = new byte[1000];
						serverSaea.SetBuffer(receiveBuffer, 0, receiveBuffer.Length);
						serverSaea.Completed += delegate { serverAre.Set(); };

						var sw = new Stopwatch();
						int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
						sw.Start();

						for (int i = 0; i < 1_000_000; i++)
						{
							if (client.SendAsync(clientSaea)) clientAre.WaitOne();
							if (clientSaea.SocketError != SocketError.Success) throw new SocketException((int)clientSaea.SocketError);

							if (server.ReceiveAsync(serverSaea)) serverAre.WaitOne();
							if (serverSaea.SocketError != SocketError.Success) throw new SocketException((int)clientSaea.SocketError);
						}

						Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
					}
				}
			}

		}

		public async static Task StreamWriteCopyTo()
		{
			using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				listener.Bind(new IPEndPoint(IPAddress.Loopback, 0));
				listener.Listen(1);

				Task connectTask = Task.Run(() => client.Connect(listener.LocalEndPoint));
				using (Socket server = listener.Accept())
				{
					await connectTask;

					using (var serverStream = new NetworkStream(server))
					using (var clientStream = new NetworkStream(client))
					{
						Task serverCopyAll = serverStream.CopyToAsync(Stream.Null);

						byte[] data = new byte[1024];
						new Random().NextBytes(data);

						var sw = new Stopwatch();
						int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
						sw.Start();

						for (int i = 0; i < 1_000_000; i++)
						{
							await clientStream.WriteAsync(data, 0, data.Length);
						}
						client.Shutdown(SocketShutdown.Send);
						serverCopyAll.Wait();
						sw.Stop();

						Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
					}
				}

			}
		}

		public async static Task SslNetworkStream()
		{
			var certCollection = new X509Certificate2Collection();
			certCollection.Import("testcert.pfx", "testcertificate", X509KeyStorageFlags.DefaultKeySet);
			var serverCert = certCollection.Cast<X509Certificate2>().First(c => c.HasPrivateKey);

			using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				listener.Bind(new IPEndPoint(IPAddress.Loopback, 0));
				listener.Listen(1);

				Task connectTask = Task.Run(() => client.Connect(listener.LocalEndPoint));
				using (Socket server = listener.Accept())
				{
					await connectTask;

					using (var serverStream = new SslStream(new NetworkStream(server, true), false, delegate { return true; }))
					using (var clientStream = new SslStream(new NetworkStream(client, true), false, delegate { return true; }))
					{
						await Task.WhenAll(
							serverStream.AuthenticateAsServerAsync(serverCert, false, SslProtocols.Tls12, false),
							clientStream.AuthenticateAsClientAsync("localhost", null, SslProtocols.Tls12, false));

						Task serverCopyAll = serverStream.CopyToAsync(Stream.Null);

						byte[] data = new byte[1024];
						new Random().NextBytes(data);

						var sw = new Stopwatch();
						int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
						sw.Start();

						for (int i = 0; i < 1_000_000; i++)
						{
							clientStream.Write(data, 0, data.Length);
						}
						clientStream.Dispose();

						serverCopyAll.Wait();
						sw.Stop();

						Console.WriteLine($"Elapsed={sw.Elapsed} Gen0={GC.CollectionCount(0) - gen0} Gen1={GC.CollectionCount(1) - gen1} Gen2={GC.CollectionCount(2) - gen2}");
					}
				}
			}

		}
	}
}
