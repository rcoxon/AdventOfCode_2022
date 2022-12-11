<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.Controls</Namespace>
</Query>

void Main()
{
	var filePath = new FilePicker().Dump("Pick a file...");
	filePath.Text = @"C:\AdventOfCode\2022\Day11s.txt";

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
/* Sample data
Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1
*/

// You can define other methods, fields, classes and namespaces here
public void Execute(string filePath)
{
	var input = File.ReadAllLines(@filePath);
	//Console.WriteLine($"Rows: {input.Length}");
	//Console.WriteLine($"Monkeys: {Math.Round(input.Length / 7.0)}");
	//Console.ReadLine();
	//foreach (var line in input)
	//{
	//	Console.WriteLine($"{line}");
	//}
	var monkeys = LoadMonkeys(File.ReadAllLines(@filePath));
	
	// Least Common Multiple needed for Part 2, but changed to use it for Part 1, too.
	var leastCommonMultiple = 1L;
	foreach (var monkey in monkeys)
		leastCommonMultiple *= Convert.ToInt64(monkey.Value.Test);
	Console.WriteLine($"LeastCommonMultiple: {leastCommonMultiple}");

	//Console.ReadLine();

	for (var i = 1; i < 21; i++)
	{
		Round(monkeys, true, leastCommonMultiple);
		
		// When testing, uncomment below to see intermediate results
		//Console.WriteLine($"After Round {i}");
		//foreach (var monkey in monkeys)
		//{
		//	Console.Write($"Monkey {monkey.Key} Inspections: {monkey.Value.Inspections} Items: ");
		//	foreach (var item in monkey.Value.Items)
		//	{
		//		Console.Write($"{item}  ");
		//	}
		//	Console.WriteLine();
		//}
	}

	var top2 = monkeys.Values
		.OrderByDescending(x => x.Inspections)
		.Take(2)
		.ToArray();
	Console.WriteLine($"Part 1: Top two inspections Monkey {top2[0].Inspections} * {top2[1].Inspections} = {top2[0].Inspections * top2[1].Inspections}");

	// Part two
	monkeys.Clear();
	monkeys = LoadMonkeys(File.ReadAllLines(@filePath));

	//Console.ReadLine();

	for (var i = 1; i < 10001; i++)
	{
		Round(monkeys, false, leastCommonMultiple);
		// When testing, uncomment below to see intermediate results
		//if (i == 1 || i == 20 || i == 1000 || i == 2000 || i == 3000 || i == 4000 || i == 5000 || i == 6000 || i == 7000 || i == 8000 || i == 9000 || i == 10000)
		//{
		//	Console.WriteLine($"After Round {i}");
		//	foreach (var monkey in monkeys)
		//	{
		//		Console.Write($"Monkey {monkey.Key} Inspections: {monkey.Value.Inspections} Items: ");
		//		//foreach (var item in monkey.Value.Items)
		//		//{
		//		//    Console.Write($"{item}  ");
		//		//}
		//		Console.WriteLine();
		//	}
		//	//Console.WriteLine("Press any key to continue...");
		//	//Console.ReadLine();
		//}
	}

	top2 = monkeys.Values
		.OrderByDescending(x => x.Inspections)
		.Take(2)
		.ToArray();

	long levelOfMonkeyBusiness = (long)top2[0].Inspections * (long)top2[1].Inspections;
	Console.WriteLine($"Part 2: Top two inspections Monkey {top2[0].Inspections} * {top2[1].Inspections} = {levelOfMonkeyBusiness}");
}

void Round(Dictionary<int, Monkey> monkeys, bool useWorryDivider, long leastCommonMultiple )
{
    foreach (var monkey in monkeys)
    {
        //Console.WriteLine($"{monkey.Key}");
        while (monkey.Value.Items.Count > 0)
        {
            monkey.Value.Inspections += 1;
            // Monkey inspects item
            var item = monkey.Value.Items.Dequeue();
            UInt64 worryLevel = Convert.ToUInt64(item);

            worryLevel %= (ulong)leastCommonMultiple;

            
            UInt64 operand2 = 0;
            operand2 = monkey.Value.Operand2 == "old" ? worryLevel : Convert.ToUInt64(monkey.Value.Operand2);
            switch (monkey.Value.Operation)
            {
                case "+":
                    worryLevel += operand2;
                    break;
                case "-":
                    worryLevel -= operand2;
                    break;
                case "*":
                    worryLevel *= operand2;
                    break;
                case "/":
                    worryLevel /= operand2;
                    break;
            }

            //Console.WriteLine($"Item: {item} Worry Level: {worryLevel}");
            if (useWorryDivider)
            {
                worryLevel /= 3;
            }

			//Console.WriteLine($"Item: {item} Worry Level: after boredom; {worryLevel}");
			UInt64 remainder = worryLevel % Convert.ToUInt64(monkey.Value.Test);
			//Console.WriteLine($"Item: {item} Worry Level: after boredom; {worryLevel} Remainder {remainder} after divide by {monkey.Value.Test}");
			var targetMonkey = remainder == 0 ? monkey.Value.TrueTarget : monkey.Value.FalseTarget;
			//Console.WriteLine($"Item: {item} Worry Level: after boredom; {worryLevel} Remainder {remainder} after divide by {monkey.Value.Test} Throw to Monkey: {targetMonkey}");
			monkeys[targetMonkey].Items.Enqueue(worryLevel);
		}
		//Monkey 0:
		//  Monkey inspects an item with a worry level of 79.
		//    Worry level is multiplied by 19 to 1501.
		//    Monkey gets bored with item. Worry level is divided by 3 to 500.
		//    Current worry level is not divisible by 23.
		//    Item with worry level 500 is thrown to monkey 3.
		//  Monkey inspects an item with a worry level of 98.
		//    Worry level is multiplied by 19 to 1862.
		//    Monkey gets bored with item. Worry level is divided by 3 to 620.
		//    Current worry level is not divisible by 23.
		//    Item with worry level 620 is thrown to monkey 3.        
	}
}
Dictionary<int, Monkey> LoadMonkeys(string[] input)
{
	var monkeys = new Dictionary<int, Monkey>();
	for (var i = 0; i < Math.Round(input.Length / 7.0); i++)
	{
		var monkey = new Monkey
		{
			Items = new Queue(),
			Inspections = 0
		};

		// Starting items
		//  Starting items: 84, 94, 94, 81, 98, 75
		foreach (var item in input[i * 7 + 1].TrimStart("  Starting items: ".ToCharArray()).Split(","))
		{
			monkey.Items.Enqueue(item.Trim());
		}

		//  Operation: new = old * 17
		var ops = input[i * 7 + 2].TrimStart("  Operation: ".ToCharArray()).Split(" ");
		monkey.Operation = ops[3];
		monkey.Operand1 = ops[2];
		monkey.Operand2 = ops[4];

		//  Test: divisible by 13
		var test = input[i * 7 + 3].TrimStart("  Test: ".ToCharArray()).Split(" ");
		monkey.Test = test[2];
		monkey.OperandTest = test[0];

		//    If true: throw to monkey 5
		var trueTarget = input[i * 7 + 4].TrimStart("    If true: ".ToCharArray()).Split(" ");
		monkey.TrueTarget = Convert.ToInt32(trueTarget[3]);

		//    If false: throw to monkey 2     
		var falseTarget = input[i * 7 + 5].TrimStart("    If false: ".ToCharArray()).Split(" ");
		monkey.FalseTarget = Convert.ToInt32(falseTarget[3]);

		monkeys.Add(i, monkey);
	}

	return monkeys;
}

public class Monkey
{
	public Queue Items { get; set; }
	public string Operation { get; set; }
	public string Operand1 { get; set; }
	public string Operand2 { get; set; }
	public string Test { get; set; }
	public string OperandTest { get; set; }
	public int TrueTarget { get; set; }
	public int FalseTarget { get; set; }
	public int Inspections { get; set; }
}