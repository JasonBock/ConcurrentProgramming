using BenchmarkDotNet.Running;
using PlayingWithLocks;
using Spackle;

await BashLedgerAsync(new MonitorLedger(100)).ConfigureAwait(false);
//await Program.BashLedgerAsync(new SpinLockLedger(100));
//await Program.BashLedgerAsync(new SpinLockLedger(100));
//BenchmarkRunner.Run<SpinLockPerformance>();

static async Task BashLedgerAsync(ILedger ledger)
{
	var manipulators = new List<Task>();

	for (var i = 0; i < 10; i++)
	{
		manipulators.Add(Task.Run(() => ManipulateLedger(ledger)));
	}

	await Task.WhenAll(manipulators).ConfigureAwait(false);
	await Console.Out.WriteLineAsync($"Final value: {ledger.Value}").ConfigureAwait(false);
}

static void ManipulateLedger(ILedger ledger)
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
