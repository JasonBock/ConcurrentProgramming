using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace PlayingWithStructures
{
	class Program
	{
		static void Main() =>
			Program.WorkWithStacks();

		private static void WorkWithStacks()
		{
			Console.Out.WriteLine(
				$"{nameof(Program.WorkWithStacks)} - start");

			var guidsInStack = new Stack<Guid>();

			for (var i = 0; i < 100; i++)
			{
				guidsInStack.Push(Guid.NewGuid());
			}

			Console.Out.WriteLine(
				$"{nameof(guidsInStack)}.Pop() is {guidsInStack.Pop()}");

			var guidsInConcurrentStack = new ConcurrentStack<Guid>();

			for (var i = 0; i < 100; i++)
			{
				guidsInConcurrentStack.Push(Guid.NewGuid());
			}

			if(guidsInConcurrentStack.TryPop(out var concurrentResult))
			{
				Console.Out.WriteLine(
					$"{nameof(guidsInConcurrentStack)}.TryPop() is {concurrentResult}");
			}

			var guidsInImmutableStack = ImmutableStack<Guid>.Empty;

			for (var i = 0; i < 100; i++)
			{
				guidsInImmutableStack = guidsInImmutableStack.Push(Guid.NewGuid());
			}

			guidsInImmutableStack = guidsInImmutableStack.Pop(
				out var immutableResult);

			Console.Out.WriteLine(
				$"{nameof(guidsInImmutableStack)}.Pop() is {immutableResult}");

			Console.Out.WriteLine(
				$"{nameof(Program.WorkWithStacks)} - finish");
		}
	}
}