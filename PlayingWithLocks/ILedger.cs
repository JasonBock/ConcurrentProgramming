namespace PlayingWithLocks;

public interface ILedger
{
	decimal Value { get; }

	void Credit(decimal value);
	void Debit(decimal value);
}