using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PlayingWithAsyncAwaitTasks
{
	class Program
	{
		private static async Task Main()
		{
			Console.Out.WriteLine(
				$"{nameof(Program.Main)} thread id: {Thread.CurrentThread.ManagedThreadId}");
			await Program.ReadFileAsync();
		}

		private static async Task ReadFileAsync()
		{
			await Console.Out.WriteLineAsync(
				$"{nameof(Program.ReadFileAsync)} thread id: {Thread.CurrentThread.ManagedThreadId}");

			using (var stream = new StreamReader("lines.txt"))
			//using (var stream = new StreamReader(new FileStream("lines.txt", FileMode.Open)))
			{
				while (!stream.EndOfStream)
				{
					var line = await stream.ReadLineAsync();
					await Console.Out.WriteLineAsync(
						$"\tAfter {nameof(Program.ReadFileAsync)}, thread id: {Thread.CurrentThread.ManagedThreadId}");
					await Console.Out.WriteLineAsync(line);
					await Console.Out.WriteLineAsync(
						$"\tAfter {nameof(Program.ReadFileAsync)}, thread id: {Thread.CurrentThread.ManagedThreadId}");
				}
			}
		}
	}
}