<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.Controls</Namespace>
</Query>

void Main()
{
	var filePath = new FilePicker().Dump("Pick a file...");
	
	filePath.Text = @"C:\AdventOfCode\2022\Day10_1_Sample.txt";

	var button = new Button("Execute").Dump();
	button.Click += (sender, args) =>
	{
		if (!string.IsNullOrEmpty(filePath.Text))
		{
			Execute(filePath.Text);
		}
		else
		{
			Console.WriteLine("No file selected!");
		}
	};

}

/*
Part 2 result:
###...##...##..####.#..#.#....#..#.####.
#..#.#..#.#..#.#....#.#..#....#..#.#....
###..#..#.#....###..##...#....####.###..
#..#.####.#....#....#.#..#....#..#.#....
#..#.#..#.#..#.#....#.#..#....#..#.#....
###..#..#..##..####.#..#.####.#..#.#....

Characters:
BACEKLHF
*/

// You can define other methods, fields, classes and namespaces here
public void Execute(string filePath)
{
	var regX = 1;
	var cycle = 0;
	var signalStrength = 0;
	var signalStrengthSum = 0;
	var checkPoint = 20;
	var input = File.ReadAllLines(filePath);
	foreach (var line in input)
	{
		// Parse the instruction
		var inst = new Instruction(line);
		
		// Execute the instruction
		for (int i = 0; i < inst.Cycles; i++)
		{
			var regXDuring = regX;
			cycle += 1;
			if (i == 0)
			{
				signalStrength = cycle * regX;
			}
			else
			{
				regX += inst.Value;
				signalStrength = cycle * regX;
			}

			// for Part 1, check at 20 cycles and then every 40 cycles
			if (cycle == checkPoint)
			{
				//Console.WriteLine($"cycle: {cycle} regX During: {regXDuring} signalStrengthDuring: {cycle * regXDuring} regX: {regX} signalStrength: {signalStrength}");
				signalStrengthSum += cycle * regXDuring; //signalStrength;
				checkPoint += 40;
			}
			
			// for Part 2, paint a 40 character wide screen
			
			// Position across 40 character screen
			var xPos = cycle % 40;
			
			// Sprite is three characters starting at regXDuring the cycle
			if (xPos >= regXDuring && xPos <= regXDuring + 2) Console.Write("#");
			else Console.Write(".");
			
			// Reposition to left side of the screen after 40 characters
			if (cycle % 40 == 0) Console.WriteLine();
		}
	}

	Console.WriteLine($"Part 1 signal strength sum: {signalStrengthSum}");
}

public class Instruction
{
	public string Action { get; set; }
	public int Value { get; set; }
	public int Cycles { get; set; }

	public Instruction(string command)
	{
		var split = command.Split(" ");
		Action = split[0];
		Value = split.Length > 1 ? Convert.ToInt32(split[1]) : 0;
		Cycles = Action == "noop" ? 1 : 2;
	}
}