using Collatz;
using PlayingWithActors.Contracts;
using System;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Numerics;

namespace PlayingWithActors.Grains;

public class CollatzGrain
	: Grain, ICollatzGrain
{
	public override async Task OnActivateAsync(CancellationToken cancellationToken)
	{
		Console.WriteLine(
			$"{nameof(this.OnActivateAsync)} - ID is {this.GetGrainId().GetGuidKey()}");
		await base.OnActivateAsync(cancellationToken);
	}

	public override async Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
	{
		Console.WriteLine(
			$"{nameof(this.OnDeactivateAsync)} - ID is {this.GetGrainId().GetGuidKey()}");
		await base.OnDeactivateAsync(reason, cancellationToken);
	}

	public ValueTask<(long value, long sequenceLength)> FindLongestSequence(long start, long finish)
	{
		(long value, long sequenceLength) result = (0, 0);

		for (var i = start; i < finish; i++)
		{
			var sequence = CollatzSequenceGenerator.Generate(i);

			if (sequence.Length > result.sequenceLength)
			{
				result = (i, sequence.Length);
			}
		}

		return ValueTask.FromResult(result);
	}
}