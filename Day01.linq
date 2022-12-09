<Query Kind="Program" />

void Main()
{
	var elfTemp = 0;
	var calories = 0;

	var elfWithMax = 0;
	var elfWithMaxCalories = 0;

	var elves = new Dictionary<int, int>();

	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day01\Day01_1.txt");
	foreach (var line in input)
	{
		if (line.Length == 0)
		{
			elfTemp += 1;
			Console.WriteLine($"elf #: {elfTemp} calories: {calories}");
			elves.Add(elfTemp, calories);
			if (calories > elfWithMaxCalories)
			{
				elfWithMax = elfTemp;
				elfWithMaxCalories = calories;
			}
			calories = 0;
		}
		else
		{
			calories += Convert.ToInt32(line);
		}

		Console.WriteLine(line);
	}

	// Add the last entry
	elfTemp += 1;
	Console.WriteLine($"elf #: {elfTemp} calories: {calories}");
	elves.Add(elfTemp, calories);
	if (calories > elfWithMaxCalories)
	{
		elfWithMax = elfTemp;
		elfWithMaxCalories = calories;
	}

	Console.WriteLine($"elf with max calories #: {elfWithMax} calories: {elfWithMaxCalories}");

	var elvesTop3 = (from elf in elves
					 orderby elf.Value
						 descending
					 select elf).ToDictionary
	(
		pair => pair.Key,
		pair => pair.Value
	).Take(3).Sum(i => i.Value);

	Console.WriteLine($"Sum of top 3: {elvesTop3}");
}

// You can define other methods, fields, classes and namespaces here