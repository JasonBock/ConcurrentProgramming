using BenchmarkDotNet.Running;
using Nito.AsyncEx;
using Spackle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayingWithLocks
{
	class Program
	{
		// C# 7.1 will let you create "async Main".
		// Until then...
		static void Main(string[] args)
		{
#pragma warning disable IDE0022 // Use expression body for methods
			//AsyncContext.Run(() => Program.MainAsync(args));
			BenchmarkRunner.Run<SpinLockPerformance>();
#pragma warning restore IDE0022 // Use expression body for methods
		}

		private static async Task MainAsync(string[] args)
		{
#pragma warning disable IDE0022 // Use expression body for methods
			await Program.BashLedgerAsync(new MonitorLedger(100));
			//await Program.BashLedgerAsync(new SpinLockLedger(100));
#pragma warning restore IDE0022 // Use expression body for methods
		}

		private static async Task BashLedgerAsync(ILedger ledger)
		{
			var manipulators = new List<Task>();

			for (var i = 0; i < 10; i++)
			{
				manipulators.Add(Task.Run(() => Program.ManipulateLedger(ledger)));
			}

			await Task.WhenAll(manipulators);
			Console.Out.WriteLine($"Final value: {ledger.Value}");
		}

		private static void ManipulateLedger(ILedger ledger)
		{
			var random = new SecureRandom();

			for (var i = 0; i < 1000; i++)
			{
				if (random.NextBoolean())
				{
					ledger.Credit((decimal)random.Next(1, 100));
				}
				else
				{
					ledger.Debit((decimal)random.Next(1, 100));
				}
			}
		}
	}
}