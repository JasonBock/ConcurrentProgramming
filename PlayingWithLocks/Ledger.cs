namespace PlayingWithLocks
{
	public sealed class Ledger
	{
		private readonly object @lock = new object();

		public Ledger(decimal value) => this.Value = value;

		public void Credit(decimal value)
		{
			lock (this.@lock)
			{
				this.Value -= value;
			}
		}

		public void Debit(decimal value)
		{
			lock(this.@lock)
			{
				this.Value += value;
			}
		}

		public decimal Value { get; private set; }
	}
}
