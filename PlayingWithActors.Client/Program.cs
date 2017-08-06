using Orleans;
using Orleans.Runtime.Configuration;
using PlayingWithActors.Contracts;
using System;
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
			var collatz = GrainClient.GrainFactory.GetGrain<ICollatzGrain>(collatzId);
			var result = collatz.CalculateIterationCountAsync(
				BigInteger.Parse("473891748")).Result;

			Console.Out.WriteLine(result);
		}
	}
}
