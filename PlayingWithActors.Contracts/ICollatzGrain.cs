namespace PlayingWithActors.Contracts;

[Alias("PlayingWithActors.Contracts.ICollatzGrain")]
public interface ICollatzGrain
	: IGrainWithGuidKey
{
   [Alias("CalculateIterationCountAsync")]
   ValueTask<int> CalculateIterationCountAsync(string value);
}