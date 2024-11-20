namespace PlayingWithActors.Contracts;

[Alias("PlayingWithActors.Contracts.ICollatzGrain")]
public interface ICollatzGrain
	: IGrainWithGuidKey
{
   [Alias("FindLongestSequence")]
   ValueTask<(long value, long sequenceLength)> FindLongestSequence(long start, long finish);
}