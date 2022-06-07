namespace PlayingWithLocks;

public sealed class SpinLockLedger
	: ILedger
{
	private SpinLock @lock;

	public SpinLockLedger(decimal value) => this.Value = value;

	public void Credit(decimal value)
	{
		var isLockAcquired = false;

		try
		{
			this.@lock.Enter(ref isLockAcquired);
			this.Value += value;
		}
		finally
		{
			if (isLockAcquired)
			{
				this.@lock.Exit();
			}
		}
	}

	public void Debit(decimal value)
	{
		var isLockAcquired = false;

		try
		{
			this.@lock.Enter(ref isLockAcquired);
			this.Value -= value;
		}
		finally
		{
			if (isLockAcquired)
			{
				this.@lock.Exit();
			}
		}
	}

	public decimal Value { get; private set; }
}