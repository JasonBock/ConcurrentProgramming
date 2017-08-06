﻿using Orleans;
using PlayingWithActors.Contracts;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayingWithActors.Grains
{
	public class CollatzGrain
		: Grain, ICollatzGrain
	{
		public async Task<BigInteger> CalculateIterationCountAsync(BigInteger value)
		{
			var iterations = BigInteger.Zero;

			while (value > 1)
			{
				value = value % 2 == 0 ?
					value / 2 : ((3 * value) + 1) / 2;
				iterations++;
				await Task.Delay(5);
			}

			return iterations;
		}
	}
}