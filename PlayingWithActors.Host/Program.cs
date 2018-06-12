using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using PlayingWithActors.Grains;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PlayingWithActors.Host
{
	class Program
	{
		static async Task Main()
		{
			var builder = new SiloHostBuilder()
				.UseLocalhostClustering()
				.Configure<EndpointOptions>(
					options => options.AdvertisedIPAddress = IPAddress.Loopback)
				.ConfigureApplicationParts(
					parts => parts.AddApplicationPart(typeof(CollatzGrain).Assembly).WithReferences());

			var host = builder.Build();
			await host.StartAsync();

			await Console.Out.WriteLineAsync("Orleans silo is running.");
			await Console.Out.WriteLineAsync("Press Enter to terminate...");
			await Console.In.ReadLineAsync();

			await host.StopAsync();
			await Console.Out.WriteLineAsync("Orleans silo is terminated.");
		}
	}
}