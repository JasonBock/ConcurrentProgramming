using Orleans;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayingWithActors.Contracts
{
	public interface ICollatzGrain
		: IGrainWithGuidKey
	{
		Task<BigInteger> CalculateIterationCountAsync(BigInteger value);
	}
}