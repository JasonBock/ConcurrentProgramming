CreateNewThreads();
CreateNewThreadsViaPool();
CreateNewThreadsViaPoolAndWait();

static void CreateNewThreads()
{
	var threads = new Thread[64];

	for (var i = 0; i < threads.Length; i++)
	{
		threads[i] = new Thread(DoThreading);
		threads[i].Start();
	}

	foreach (var thread in threads)
	{
		thread.Join();
	}
}

static void CreateNewThreadsViaPool()
{
	for (var i = 0; i < 64; i++)
	{
		ThreadPool.QueueUserWorkItem(DoThreadingWithData, i);
	}
}

static void CreateNewThreadsViaPoolAndWait()
{
	var waits = new EventWaitHandle[64];

	for (var i = 0; i < 64; i++)
	{
		var wait = new ManualResetEvent(false);
		waits[i] = wait;
		ThreadPool.QueueUserWorkItem(
			DoThreadingWithThreadingData, new ThreadingData(wait, i));
	}

	WaitHandle.WaitAll(waits);
}

static void DoThreading() =>
	Console.Out.WriteLine($"Current thread ID: {Environment.CurrentManagedThreadId}");

static void DoThreadingWithData(object? id) =>
	Console.Out.WriteLine($"Current id: {id}, thread ID: {Environment.CurrentManagedThreadId}");

static void DoThreadingWithThreadingData(object? data)
{
	var value = (data as ThreadingData)!;
	Console.Out.WriteLine($"Current id: {value.Id}, thread ID: {Environment.CurrentManagedThreadId}");
	value.Wait.Set();
}

internal class ThreadingData
{
	internal ThreadingData(EventWaitHandle wait, int id)
	{
		this.Wait = wait;
		this.Id = id;
	}

	internal int Id { get; }
	internal EventWaitHandle Wait { get; }
}