using BenchmarkDotNet.Attributes;

namespace PlayingWithLocks
{
	[MemoryDiagnoser]
	public class SpinLockPerformance
	{
		private ILedger monitorLedger;
		private ILedger spinLockLedger;

		[GlobalSetup]
		public void GlobalSetup()
		{
			this.monitorLedger = new MonitorLedger(100);
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
		public decimal CreditAndDebitSpinLockLedger()
		{
			this.spinLockLedger.Credit(200);
			this.spinLockLedger.Debit(50);
			return this.spinLockLedger.Value;
		}
	}
}