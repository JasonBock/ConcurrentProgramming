await Console.Out.WriteLineAsync(
	$"Current thread id: {Environment.CurrentManagedThreadId}");

using var stream = new StreamReader(new FileStream("lines.txt", FileMode.Open));
//using var stream = new StreamReader("lines.txt");

while (!stream.EndOfStream)
{
	var line = await stream.ReadLineAsync();
	await Console.Out.WriteLineAsync(
		$"\tAfter stream.ReadLineAsync(), thread id: {Environment.CurrentManagedThreadId}");
	await Console.Out.WriteLineAsync(line);
	await Console.Out.WriteLineAsync(
		$"\tAfter Console.Out.WriteLineAsync, thread id: {Environment.CurrentManagedThreadId}");
}