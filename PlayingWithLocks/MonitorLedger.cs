namespace PlayingWithLocks;

public sealed class MonitorLedger
	: ILedger
{
	private readonly object @lock = new object();

	public MonitorLedger(decimal value) => this.Value = value;

	public void Credit(decimal value)
	{
		lock (this.@lock)
		{
			this.Value += value;
		}
	}

	public void Debit(decimal value)
	{
		lock (this.@lock)
		{
			this.Value -= value;
		}
	}

	public decimal Value { get; private set; }
}