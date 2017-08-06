using Orleans;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayingWithActors
{
	public interface ICollatzGrain
		: IGrainWithStringKey
	{
		Task<BigInteger> CalculateIterationCountAsync(BigInteger value);
	}
}
