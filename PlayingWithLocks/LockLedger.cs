
namespace PlayingWithLocks;

public sealed class LockLedger
	: ILedger
{
	private readonly Lock @lock = new();

	public LockLedger(decimal value) => this.Value = value;

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