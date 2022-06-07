using Orleans;
using Orleans.Runtime;
using PlayingWithActors.Contracts;
using Polly;
using System.Globalization;
using System.Numerics;

const int RetryCount = 5;
var Retry = TimeSpan.FromSeconds(1);

var policy = Policy.Handle<SiloUnavailableException>().WaitAndRetryAsync(
	RetryCount, retryAttempt =>
	{
		return Retry;
	});
var client = await policy.ExecuteAsync(GetClientAsync).ConfigureAwait(false);

await Console.Out.WriteLineAsync("Begin...").ConfigureAwait(false);

var collatzId = Guid.NewGuid();
var tasks = new List<Task>
{
	RunCalculation(collatzId, client),
	RunCalculation(collatzId, client),
	RunCalculation(collatzId, client)
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

static Task RunCalculation(Guid collatzId, IClusterClient client) =>
	Task.Run(() =>
	{
		// Make a shared grain...
		var collatz = client.GetGrain<ICollatzGrain>(collatzId);

		// Make a unique grain...
		//var collatz = client.GetGrain<ICollatzGrain>(Guid.NewGuid());

		var result = collatz.CalculateIterationCountAsync(
			BigInteger.Parse("5731897498317984739817498317", CultureInfo.InvariantCulture)).Result;

		Console.Out.WriteLine($"Result is {result}");
	});