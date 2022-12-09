<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day06_1.txt");
	foreach (var line in input)
	{
		//Console.WriteLine(line);
		var i = 0;
		var q = new Queue<char>();
		foreach (var c in line)
		{
			i++;
			if (q.Count == 4) q.Dequeue();
			q.Enqueue(c);
			if (q.Distinct().Count() == 4)
			{
				Console.Write($"Character Count: {i} start-of-packet marker:");
				foreach (var c1 in q)
				{
					Console.Write($"{c1}");
				}
				Console.WriteLine();
				break;
			}

		}

		i = 0;
		q.Clear();
		foreach (var c in line)
		{
			i++;
			if (q.Count == 14) q.Dequeue();
			q.Enqueue(c);
			if (q.Distinct().Count() == 14)
			{
				Console.Write($"Character Count: {i} start-of-message marker:");
				foreach (var c1 in q)
				{
					Console.Write($"{c1}");
				}
				Console.WriteLine();
				break;
			}

		}

	}
}

// You can define other methods, fields, classes and namespaces here