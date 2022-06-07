using System.Threading;

namespace PlayingWithLocks;

public sealed class IntegerFieldInterlockedLedger
{
	private int value;

	public IntegerFieldInterlockedLedger(int value) => this.value = value;

	public void Credit(int value) =>
		Interlocked.Add(ref this.value, value);

	public void Debit(int value) =>
		Interlocked.Add(ref this.value, value * -1);

	public int Value => this.value;
}