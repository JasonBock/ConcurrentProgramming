using Nito.AsyncEx;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithAsyncAwaitTasks
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Out.WriteLine($"Main thread id: {Thread.CurrentThread.ManagedThreadId}");
			AsyncContext.Run(() => Program.MainAsync(args));
			var c = Task.Run(() => { });
			var c2 = Task.Run(() => { });

			Task.WhenAll(c, c2);
		}

		private static async Task MainAsync(string[] args)
		{
			Console.Out.WriteLine($"MainAsync thread id: {Thread.CurrentThread.ManagedThreadId}");
			await Program.ReadFileAsync();
		}

		private static async Task ReadFileAsync()
		{
			await Console.Out.WriteLineAsync($"ReadFileAsync thread id: {Thread.CurrentThread.ManagedThreadId}");

			//using (var stream = new StreamReader("lines.txt"))
			using (var stream = new StreamReader(new FileStream("lines.txt", FileMode.Open)))
			{
				while (!stream.EndOfStream)
				{
					var line = await stream.ReadLineAsync();
					await Console.Out.WriteLineAsync(
						$"ReadFileAsync - after ReadLineAsync(), thread id: {Thread.CurrentThread.ManagedThreadId}");
					await Console.Out.WriteLineAsync(line);
					await Console.Out.WriteLineAsync(
						$"ReadFileAsync - after WriteLineAsync(), thread id: {Thread.CurrentThread.ManagedThreadId}");
				}
			}
		}
	}
}