using BenchmarkDotNet.Attributes;

namespace PlayingWithLocks;

[MemoryDiagnoser]
public class SpinLockPerformance
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CA1859 // Use concrete types when possible for improved performance
   private ILedger monitorLedger;
	private ILedger lockLedger;
	private ILedger spinLockLedger;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	[GlobalSetup]
	public void GlobalSetup()
	{
		this.monitorLedger = new MonitorLedger(100);
		this.lockLedger = new LockLedger(100);
		this.spinLockLedger = new SpinLockLedger(100);
	}

	[Benchmark]
	public decimal CreditAndDebitMonitorLedger()
	{
		this.monitorLedger.Credit(200);
		this.monitorLedger.Debit(50);
		return this.monitorLedger.Value;
	}

	[Benchmark]
	public decimal CreditAndDebitLockLedger()
	{
		this.lockLedger.Credit(200);
		this.lockLedger.Debit(50);
		return this.lockLedger.Value;
	}

	[Benchmark]
	public decimal CreditAndDebitSpinLockLedger()
	{
		this.spinLockLedger.Credit(200);
		this.spinLockLedger.Debit(50);
		return this.spinLockLedger.Value;
	}
}