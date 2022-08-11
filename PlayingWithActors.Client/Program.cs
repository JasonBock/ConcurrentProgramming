using Orleans;
using Orleans.Runtime.Messaging;
using PlayingWithActors.Contracts;
using Polly;
using System.Globalization;
using System.Numerics;

const int RetryCount = 5;
var Retry = TimeSpan.FromSeconds(1);

var policy = Policy.Handle<ConnectionFailedException>().WaitAndRetryAsync(
	RetryCount, retryAttempt => 
	{
		Console.Out.WriteLine($"Retrying with attempt {retryAttempt} of {RetryCount}...");
		return Retry;
	});
var client = await policy.ExecuteAsync(GetClientAsync).ConfigureAwait(false);

await Console.Out.WriteLineAsync("Begin...").ConfigureAwait(false);

var sharedGrainId = Guid.NewGuid();

await Console.Out.WriteLineAsync($"Shared grain ID: {sharedGrainId}").ConfigureAwait(false);

var tasks = new List<Task>
{
	RunCalculationAsync(sharedGrainId, client),
	RunCalculationAsync(sharedGrainId, client),
	RunCalculationAsync(sharedGrainId, client),
	RunCalculationAsync(Guid.NewGuid(), client),
	RunCalculationAsync(Guid.NewGuid(), client),
	RunCalculationAsync(Guid.NewGuid(), client)
};

await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

async Task<IClusterClient> GetClientAsync()
{
	var clientBuilder = new ClientBuilder()
		.UseLocalhostClustering()
		.ConfigureApplicationParts(
			parts => parts.AddApplicationPart(typeof(ICollatzGrain).Assembly).WithReferences())
		.Build();

	await clientBuilder.Connect().ConfigureAwait(false);
	return clientBuilder;
}

static async Task RunCalculationAsync(Guid grainId, IClusterClient client)
{
	var collatz = client.GetGrain<ICollatzGrain>(grainId);

	var result = await collatz.CalculateIterationCountAsync(
		BigInteger.Parse("5731897498317984739817498317", CultureInfo.InvariantCulture)).ConfigureAwait(false);

	await Console.Out.WriteLineAsync($"Result is {result} from grain {grainId}").ConfigureAwait(false);
}