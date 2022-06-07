using System.Collections.Concurrent;
using System.Collections.Immutable;

Console.Out.WriteLine("Start");

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

if (guidsInConcurrentStack.TryPop(out var concurrentResult))
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

Console.Out.WriteLine("Finish");