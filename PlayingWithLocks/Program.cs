using BenchmarkDotNet.Running;
using Spackle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayingWithLocks
{
	class Program
	{
		//await Program.BashLedgerAsync(new MonitorLedger(100));
		//await Program.BashLedgerAsync(new SpinLockLedger(100));
		//static async Task Main() =>
		//	await Program.BashLedgerAsync(new SpinLockLedger(100));

		static void Main() =>
			BenchmarkRunner.Run<SpinLockPerformance>();

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