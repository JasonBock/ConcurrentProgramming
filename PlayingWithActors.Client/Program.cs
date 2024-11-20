using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlayingWithActors.Contracts;

var builder = Host.CreateDefaultBuilder(args)
	 .UseOrleansClient(client => client.UseLocalhostClustering())
	 .ConfigureLogging(logging => logging.AddConsole())
	 .UseConsoleLifetime();

using var host = builder.Build();
await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

Console.WriteLine("Begin...");

var tasks = new List<Task>
{
	RunCalculationAsync(Guid.NewGuid(), client),
	RunCalculationAsync(Guid.NewGuid(), client),
	RunCalculationAsync(Guid.NewGuid(), client)
};

//var sharedGrainId = Guid.NewGuid();

//var tasks = new List<Task>
//{
//	RunCalculationAsync(sharedGrainId, client),
//	RunCalculationAsync(sharedGrainId, client),
//	RunCalculationAsync(sharedGrainId, client)
//};

await Task.WhenAll([.. tasks]);

static async Task RunCalculationAsync(Guid grainId, IClusterClient client)
{
	var collatz = client.GetGrain<ICollatzGrain>(grainId);

	var result = await collatz.FindLongestSequence(1_000_000, 5_000_000);

	Console.WriteLine($"Result is {result} from grain {grainId}");
}