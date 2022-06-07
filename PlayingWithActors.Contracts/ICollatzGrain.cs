using Orleans;
using System.Numerics;

namespace PlayingWithActors.Contracts;

public interface ICollatzGrain
	: IGrainWithGuidKey
{
	Task<BigInteger> CalculateIterationCountAsync(BigInteger value);
}