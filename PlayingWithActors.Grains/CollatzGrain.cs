using Orleans;
using PlayingWithActors.Contracts;
using System.Numerics;

namespace PlayingWithActors.Grains;

public class CollatzGrain
	: Grain, ICollatzGrain
{
	public override async Task OnActivateAsync()
	{
		await Console.Out.WriteLineAsync(
			$"{nameof(this.OnActivateAsync)} - primary ID is {this.GetGrainIdentity().PrimaryKey}").ConfigureAwait(false);
		await base.OnActivateAsync().ConfigureAwait(false);
	}

	public override async Task OnDeactivateAsync()
	{
		await Console.Out.WriteLineAsync(
			$"{nameof(this.OnDeactivateAsync)} - primary ID is {this.GetGrainIdentity().PrimaryKey}").ConfigureAwait(false);
		await base.OnDeactivateAsync().ConfigureAwait(false);
	}

	public async Task<BigInteger> CalculateIterationCountAsync(BigInteger value)
	{
		var iterations = BigInteger.Zero;

		while (value > 1)
		{
			value = value % 2 == 0 ?
				value / 2 : ((3 * value) + 1) / 2;
			iterations++;
			await Task.Delay(5).ConfigureAwait(false);
		}

		return iterations;
	}
}