<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day05_1.txt");
	var rackInput = new Stack<string>();
	var instructions = new Dictionary<int, Instruction>();
	var count = 0;
	bool instruction = false;
	foreach (var line in input)
	{
		if (line.Length == 0)
		{
			instruction = true;
			continue;
		}
		//Console.WriteLine(line);
		if (!instruction)
		{
			rackInput.Push(line);
		}
		else
		{
			var split = line.Split(' ');
			instructions.Add(count,
				new Instruction
				{
					Type = split[0],
					Quantity = Convert.ToInt32(split[1]),
					Direction = split[2],
					Location1 = Convert.ToInt32(split[3]),
					Location2 = Convert.ToInt32(split[5])
				});
			count++;
		}
	}

	var racks = new Dictionary<int, Stack<string>>();
	var racks2 = new Dictionary<int, Stack<string>>();

	foreach (var row in rackInput)
	{
		if (racks.Count == 0)
		{
			var split = row.Replace("   ", ",").Replace(" ", "").Split(",");
			foreach (var i in split)
			{
				racks.Add(Convert.ToInt32(i), new Stack<string>());
				racks2.Add(Convert.ToInt32(i), new Stack<string>());
			}
		}
		else
		{
			var split = row.Replace("    ", ",").Replace(" ", ",").Split(",");
			var i = 1;
			foreach (var crate in split)
			{
				if (crate.Contains("[")) racks[i].Push(crate.Replace("[", "").Replace("]", ""));
				if (crate.Contains("[")) racks2[i].Push(crate.Replace("[", "").Replace("]", ""));
				i++;
			}
		}
		//Console.WriteLine(row);
	}

	// Step 1 apply instructions
	foreach (var inst in instructions)
	{
		//Console.WriteLine($"Type: {inst.Value.Type} Quantity: {inst.Value.Quantity} Direction: {inst.Value.Direction} Location1: {inst.Value.Location1} Location2: {inst.Value.Location2}");
		for (int i = 0; i < inst.Value.Quantity; i++)
		{
			racks[inst.Value.Location2].Push(racks[inst.Value.Location1].Pop());
		}
	}

	Console.Write("Step 1 Result: ");
	foreach (var rack in racks)
	{
		Console.Write(rack.Value.Peek());
	}
	Console.WriteLine();
	
	// Step 2 apply instructions
	var crane = new Stack<string>();
	foreach (var inst in instructions)
	{
		//Console.WriteLine($"Type: {inst.Value.Type} Quantity: {inst.Value.Quantity} Direction: {inst.Value.Direction} Location1: {inst.Value.Location1} Location2: {inst.Value.Location2}");

		// Load crane
		for (int i = 0; i < inst.Value.Quantity; i++)
		{
			crane.Push(racks2[inst.Value.Location1].Pop());
		}

		// Unload crane
		var crateCount = crane.Count;
		for (int i = 0; i < crateCount; i++)
		{
			racks2[inst.Value.Location2].Push(crane.Pop());
		}
	}

	Console.Write("Step 2 Result: ");
	foreach (var rack in racks2)
	{
		Console.Write(rack.Value.Peek());
	}
}

// You can define other methods, fields, classes and namespaces here
public class Instruction
{
	//move 1 from 5 to 6
	public string Type { get; set; }
	public int Quantity { get; set; }
	public string Direction { get; set; }
	public int Location1 { get; set; }
	public int Location2 { get; set; }
}