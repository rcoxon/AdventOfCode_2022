<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day04_1.txt");
	var assignments = new List<AssignmentPair>();
	foreach (var line in input)
	{
		//Console.WriteLine(line);
		assignments.Add(new AssignmentPair(line));
	}

	var count = assignments.Count(a => a.Begin1 <= a.Begin2 && a.End1 >= a.End2 || a.Begin2 <= a.Begin1 && a.End2 >= a.End1);

	Console.WriteLine($"Part 1 Assignemnt Pairs: {count}");

	var count2 = assignments.Count(a =>
		a.Begin2 <= a.Begin1 && a.Begin1 <= a.End2 || // Begin1 between Begin2 and End2
		a.Begin2 <= a.End1 && a.End1 <= a.End2 || // End1 between Begin2 and End2
		a.Begin1 <= a.Begin2 && a.Begin2 <= a.End1 || // Begin2 between Begin1 and End1
		a.Begin1 <= a.End2 && a.End2 <= a.End1    // End2 between Begin1 and End2
		);

	Console.WriteLine($"part 2 Assignment Pairs: {count2}");
}

// You can define other methods, fields, classes and namespaces here
public class AssignmentPair
{
	public int Begin1 { get; set; }
	public int End1 { get; set; }
	public int Begin2 { get; set; }
	public int End2 { get; set; }

	public AssignmentPair(string input)
	{
		var split = input.Split(',');
		var pair1 = split[0].Split('-');
		var pair2 = split[1].Split('-');

		Begin1 = Convert.ToInt32(pair1[0]);
		End1 = Convert.ToInt32(pair1[1]);
		Begin2 = Convert.ToInt32(pair2[0]);
		End2 = Convert.ToInt32(pair2[1]);
	}
}