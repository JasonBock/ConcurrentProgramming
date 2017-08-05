using System;
using System.Threading;

namespace PlayingWithThreads
{
	class Program
	{
		static void Main(string[] args)
		{
#pragma warning disable IDE0022 // Use expression body for methods
			//Program.CreateNewThreads();
			Program.CreateNewThreadsViaPool();
#pragma warning restore IDE0022 // Use expression body for methods
		}

		private static void CreateNewThreads()
		{
			var threads = new Thread[64];

			for (var i = 0; i < threads.Length; i++)
			{
				threads[i] = new Thread(Program.DoThreading);
				threads[i].Start();
			}

			foreach (var thread in threads)
			{
				thread.Join();
			}
		}

		private static void CreateNewThreadsViaPool()
		{
			for(var i = 0; i < 64; i++)
			{
				ThreadPool.QueueUserWorkItem(Program.DoThreadingWithData, i);
			}
		}

		private static void DoThreading() =>
			Console.Out.WriteLine($"Current thread ID: {Thread.CurrentThread.ManagedThreadId}");

		private static void DoThreadingWithData(object id) =>
			Console.Out.WriteLine($"Current id: {id}, thread ID: {Thread.CurrentThread.ManagedThreadId}");
	}
}