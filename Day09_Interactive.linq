<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>LINQPad.Controls</Namespace>
</Query>

void Main()
{
	var filePath = new FilePicker().Dump("Pick a file...");

	var button = new Button("Execute").Dump();
	button.Click += (sender, args) =>
	{
		if (!string.IsNullOrEmpty(filePath.Text))
		{
			Execute(filePath.Text);
		}
	};

}


// You can define other methods, fields, classes and namespaces here
public void Execute(string filePath)
{
	var input = File.ReadAllLines(filePath);
	//Console.WriteLine($"Rows: {input.Length}, Columns: {input[0].Length}");
	// for Part 1
	var tailPositions = new List<Position>();
	tailPositions.Add(new Position { x = 0, y = 0 });
	Position t = new Position { x = 0, y = 0 };
	Position h = new Position { x = 0, y = 0 };

	// for Part 2
	var tail9Positions = new List<Position>();
	tail9Positions.Add(new Position { x = 0, y = 0 });
	Dictionary<int, Position> knots = new Dictionary<int, Position>
		{
			{ 0, new Position { x = 0, y = 0 } }, // Head
			{ 1, new Position { x = 0, y = 0 } },
			{ 2, new Position { x = 0, y = 0 } },
			{ 3, new Position { x = 0, y = 0 } },
			{ 4, new Position { x = 0, y = 0 } },
			{ 5, new Position { x = 0, y = 0 } },
			{ 6, new Position { x = 0, y = 0 } },
			{ 7, new Position { x = 0, y = 0 } },
			{ 8, new Position { x = 0, y = 0 } },
			{ 9, new Position { x = 0, y = 0 } }  // Tail
		};

	foreach (var line in input)
	{
		var motion = new Motion(line);

		// Execute the motion for the number of steps
		for (int i = 0; i < motion.Steps; i++)
		{
			// for Part 1
			MoveHead(h, motion.Direction);
			CheckMoveTail(h, t);

			// Check if tail is in a new unique position
			if (tailPositions.Where(p => p.x == t.x && p.y == t.y).Count() == 0)
			{
				tailPositions.Add(new Position { x = t.x, y = t.y });
			}

			// for Part 2
			MoveHead(knots[0], motion.Direction);

			// Move all the "tails"
			for (int c = 0; c < 9; c++)
			{
				CheckMoveTail(knots[c], knots[c + 1]);
			}

			// Check if tail 9 is in a new unique position
			if (tail9Positions.Where(p => p.x == knots[9].x && p.y == knots[9].y).Count() == 0)
			{
				tail9Positions.Add(new Position { x = knots[9].x, y = knots[9].y });
			}

		}
	}

	Console.WriteLine($"Part 1: Tail unique positions: {tailPositions.Count()}");
	Console.WriteLine($"Part 2: Tail (9) unique positions: {tail9Positions.Count()}");

}


void MoveHead(Position h, string direction)
{
	if (direction == "R") h.x += 1;
	if (direction == "L") h.x -= 1;
	if (direction == "U") h.y += 1;
	if (direction == "D") h.y -= 1;
}

void CheckMoveTail(Position h, Position t)
{
	// already touching
	if (Math.Abs(h.x - t.x) < 2 && Math.Abs(h.y - t.y) < 2) return;

	// on the same line
	if (h.y == t.y && h.x > t.x) { t.x += 1; return; }
	if (h.y == t.y && h.x < t.x) { t.x -= 1; return; }

	// directly above or below
	if (h.x == t.x && h.y > t.y) { t.y += 1; return; }
	if (h.x == t.x && h.y < t.y) { t.y -= 1; return; }

	// one line above and to the right or two lines above and to the right
	if (h.y > t.y && h.x > t.x) { t.y += 1; t.x += 1; return; };

	// one line above and to the left or two lines above and to the left
	if (h.y > t.y && h.x < t.x) { t.y += 1; t.x -= 1; return; };

	// one line below and to the right or two lines above and to the right
	if (h.y < t.y && h.x > t.x) { t.y -= 1; t.x += 1; return; };

	// one line below and to the left or two lines above and to the left
	if (h.y < t.y && h.x < t.x) { t.y -= 1; t.x -= 1; return; };

}

public class Position
{
	public int x { get; set; }
	public int y { get; set; }
}

public class Motion
{
	public string Direction { get; set; }
	public int Steps { get; set; }

	public Motion(string line)
	{
		var instruction = line.Split(" ");
		Direction = instruction[0];
		Steps = Convert.ToInt32(instruction[1]);
	}
}