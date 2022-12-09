<Query Kind="Program" />

void Main()
{
	var positions = "0abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

	var prioritySum = 0;

	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day03_1.txt");
	foreach (var line in input)
	{
		//Console.WriteLine($"Line: {line} - Length of line: {line.Length}");

		var first = line.Substring(0, line.Length / 2);
		var second = line.Substring(line.Length / 2, line.Length / 2);

		foreach (var c in first.Where(c => second.Contains(c)))
		{
			//Console.WriteLine($"Duplicate {c} - Priority: {positions.IndexOf(c)}");
			prioritySum += positions.IndexOf(c);
			break;
		}
	}
	
	Console.WriteLine($"Priority Sum: {prioritySum}");

	var rucksacks = new List<string>();
	foreach (var line in input)
	{
		rucksacks.Add(line);
	}

	var grouped = rucksacks.Select((x, i) => new { Index = i, Value = x })
		.GroupBy(i => i.Index / 3)
		.Select(g => g.ToList())
		.Select(g => new ElfGroup { Rucksack1 = g[0].Value, Rucksack2 = g[1].Value, Rucksack3 = g[2].Value })
		.Distinct();

	var prioritySum2 = 0;

	foreach (var group in grouped)
	{
		//Console.WriteLine($"{group.Rucksack1} {group.Rucksack2} {group.Rucksack3}");
		var c = group.Rucksack1.Where(c => group.Rucksack2.Contains(c) && group.Rucksack3.Contains(c));
		//Console.WriteLine($"Character: {c.FirstOrDefault()}");
		prioritySum2 += positions.IndexOf(c.FirstOrDefault());
	}

	Console.WriteLine($"Priority Sum Badges: {prioritySum2}");
}

// You can define other methods, fields, classes and namespaces here
public class ElfGroup
{
	public string Rucksack1 { get; set; }
	public string Rucksack2 { get; set; }
	public string Rucksack3 { get; set; }
}