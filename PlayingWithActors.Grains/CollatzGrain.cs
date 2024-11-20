using PlayingWithActors.Contracts;
using System.Globalization;
using System.Numerics;

namespace PlayingWithActors.Grains;

public class CollatzGrain
	: Grain, ICollatzGrain
{
   public override async Task OnActivateAsync(CancellationToken cancellationToken)
	{
		await Console.Out.WriteLineAsync(
			$"{nameof(this.OnActivateAsync)} - ID is {this.GetGrainId().GetGuidKey()}");
		await base.OnActivateAsync(cancellationToken);
	}

   public override async Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
	{
		await Console.Out.WriteLineAsync(
			$"{nameof(this.OnDeactivateAsync)} - ID is {this.GetGrainId().GetGuidKey()}");
		await base.OnDeactivateAsync(reason, cancellationToken);
	}

	public ValueTask<int> CalculateIterationCountAsync(string value)
	{
		var numberValue = BigInteger.Parse(value, CultureInfo.CurrentCulture);
		var iterations = 0;

		while (numberValue > 1)
		{
			numberValue = numberValue % 2 == 0 ?
				numberValue / 2 : ((3 * numberValue) + 1) / 2;
			iterations++;
		}

		return ValueTask.FromResult(iterations);
	}
}