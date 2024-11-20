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

var sharedGrainId = Guid.NewGuid();

var tasks = new List<Task>
{
	RunCalculationAsync("368497025897042654972594625799205742506794289425748658462507438917489371894631897457389056391065390165893165893016589031658903618950631890", Guid.NewGuid(), client),
	RunCalculationAsync("568497025897042654972594625799205742506794289425748658462507438917489371894631897457389056391065390165893165893016589031658903618950631890", Guid.NewGuid(), client),
	RunCalculationAsync("768497025897042654972594625799205742506794289425748658462507438917489371894631897457389056391065390165893165893016589031658903618950631890", Guid.NewGuid(), client)
};

await Task.WhenAll([.. tasks]);

static async Task RunCalculationAsync(string value, Guid grainId, IClusterClient client)
{
	var collatz = client.GetGrain<ICollatzGrain>(grainId);

	var result = await collatz.CalculateIterationCountAsync(value);

	await Console.Out.WriteLineAsync($"Result is {result} from grain {grainId}");
}