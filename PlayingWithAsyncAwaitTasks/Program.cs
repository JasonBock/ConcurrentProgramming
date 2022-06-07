await Console.Out.WriteLineAsync(
	$"Current thread id: {Environment.CurrentManagedThreadId}").ConfigureAwait(false);
await ReadFileAsync().ConfigureAwait(false);

static async Task ReadFileAsync()
{
	await Console.Out.WriteLineAsync(
		$"{nameof(ReadFileAsync)} thread id: {Environment.CurrentManagedThreadId}").ConfigureAwait(false);

	//using var stream = new StreamReader(new FileStream("lines.txt", FileMode.Open));
	using var stream = new StreamReader("lines.txt");

	while (!stream.EndOfStream)
	{
		var line = await stream.ReadLineAsync().ConfigureAwait(false);
		await Console.Out.WriteLineAsync(
			$"\tAfter {nameof(ReadFileAsync)}, thread id: {Environment.CurrentManagedThreadId}").ConfigureAwait(false);
		await Console.Out.WriteLineAsync(line).ConfigureAwait(false);
		await Console.Out.WriteLineAsync(
			$"\tAfter {nameof(ReadFileAsync)}, thread id: {Environment.CurrentManagedThreadId}").ConfigureAwait(false);
	}
}