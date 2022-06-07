namespace PlayingWithLocks;

public sealed class IntegerFieldMonitorLedger
{
	private readonly object @lock = new object();

	public IntegerFieldMonitorLedger(int value) => this.Value = value;

	public void Credit(int value)
	{
		lock (this.@lock)
		{
			this.Value += value;
		}
	}

	public void Debit(int value)
	{
		lock (this.@lock)
		{
			this.Value -= value;
		}
	}

	public int Value { get; private set; }
}