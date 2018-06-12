using Orleans;
using Orleans.Runtime;
using PlayingWithActors.Contracts;
using Polly;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayingWithActors.Client
{
	class Program
	{
		private const int RetryCount = 5;
		private static readonly TimeSpan Retry = TimeSpan.FromSeconds(1);

		static async Task Main()
		{
			async Task<IClusterClient> GetClientAsync()
			{
				var clientBuilder = new ClientBuilder()
					.UseLocalhostClustering()
					.ConfigureApplicationParts(
						parts => parts.AddApplicationPart(typeof(ICollatzGrain).Assembly).WithReferences())
					.Build();

				await clientBuilder.Connect();
				return clientBuilder;
			}

			var policy = Policy.Handle<SiloUnavailableException>().WaitAndRetryAsync(
				Program.RetryCount, retryAttempt =>
				{
					return Program.Retry;
				});
			var client = await policy.ExecuteAsync(GetClientAsync);

			Console.Out.WriteLine("Begin...");

			var collatzId = Guid.NewGuid();
			var tasks = new List<Task>
			{
				Program.RunCalculation(collatzId, client),
				Program.RunCalculation(collatzId, client),
				Program.RunCalculation(collatzId, client)
			};

			Task.WaitAll(tasks.ToArray());
		}

		private static Task RunCalculation(Guid collatzId, IClusterClient client) =>
			Task.Run(() =>
			{
				// Make a shared grain...
				var collatz = client.GetGrain<ICollatzGrain>(collatzId);
				
				// Make a unique grain...
				//var collatz = client.GetGrain<ICollatzGrain>(Guid.NewGuid());

				var result = collatz.CalculateIterationCountAsync(
					BigInteger.Parse("5731897498317984739817498317")).Result;

				Console.Out.WriteLine($"Result is {result}");
			});
	}
}
