<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day09_1_Sample.txt");
	Console.WriteLine($"Rows: {input.Length}, Columns: {input[0].Length}");
	foreach (var line in input)
	{
		Console.WriteLine(line);
	}
}

// You can define other methods, fields, classes and namespaces here