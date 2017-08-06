using Orleans;
using Orleans.Runtime.Configuration;
using PlayingWithActors.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace PlayingWithActors.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = ClientConfiguration.LocalhostSilo(30000);

			while (true)
			{
				try
				{
					var client =
						 new ClientBuilder().UseConfiguration(config).Build();
					client.Connect().GetAwaiter().GetResult();
					break;
				}
				catch
				{
					Task.Delay(TimeSpan.FromSeconds(1));
				}
			}

			GrainClient.Initialize(config);
			Console.Out.WriteLine("Begin...");

			var collatzId = Guid.NewGuid();
			var tasks = new List<Task>
			{
				Program.RunCalculation(collatzId),
				Program.RunCalculation(collatzId),
				Program.RunCalculation(collatzId)
			};

			Task.WaitAll(tasks.ToArray());
		}

		private static Task RunCalculation(Guid collatzId) =>
			Task.Run(() =>
			{
				var collatz = GrainClient.GrainFactory.GetGrain<ICollatzGrain>(collatzId);
				var result = collatz.CalculateIterationCountAsync(
					BigInteger.Parse("5731897498317984739817498317")).Result;

				Console.Out.WriteLine($"Result is {result}");
			});
	}
}
