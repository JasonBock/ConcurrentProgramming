﻿using Orleans.Runtime.Host;
using System;

namespace PlayingWithActors.Host
{
	class Program
	{
		private static SiloHost siloHost;

		static void Main(string[] args)
		{
			Program.InitializeSilo();
			Console.WriteLine("Orleans silo is running.");
			Console.WriteLine("Press Enter to terminate...");
			Console.ReadLine();
			Program.ShutdownSilo();
		}

		private static void InitializeSilo()
		{
			Program.siloHost = new SiloHost("CollatzSilo")
			{
				ConfigFileName = "OrleansConfiguration.xml"
			};
			Program.siloHost.InitializeOrleansSilo();

			if (!siloHost.StartOrleansSilo())
			{
				throw new SystemException(
					$"Failed to start Orleans silo '{siloHost.Name}' as a {siloHost.Type} node");
			}
		}

		private static void ShutdownSilo()
		{
			if (Program.siloHost != null)
			{
				siloHost.Dispose();
				GC.SuppressFinalize(siloHost);
				siloHost = null;
			}
		}
	}
}
