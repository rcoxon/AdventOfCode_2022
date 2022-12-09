<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day08_1.txt");
	Console.WriteLine($"Rows: {input.Length}, Columns: {input[0].Length}");
	var trees = new List<Tree>();

	// Load trees list
	for (int y = 0; y < input.Length; y++)
	{
		for (int x = 0; x < input[0].Length; x++)
		{
			trees.Add(new Tree
			{
				h = Convert.ToInt32(input[y].Substring(x, 1)),
				x = x,
				y = y,
				v = false
			});
		}

	}

	// Calculate visibility
	foreach (var tree in trees)
	{
		// Trees on edges are always visible
		if (tree.x == 0 || tree.x == input[0].Length - 1)
		{
			tree.v = true;
			continue;
		}

		// Trees on top/bottom are always visible
		if (tree.y == 0 || tree.y == input.Length - 1)
		{
			tree.v = true;
			continue;
		}

		// Check visibility to the left
		if (!trees.Exists(t => t.y == tree.y &&
							   t.x < tree.x &&
							   t.h >= tree.h))
		{
			tree.v = true;
			continue;
		}

		// Check visibility to the right
		if (!trees.Exists(t => t.y == tree.y &&
							   t.x > tree.x &&
							   t.h >= tree.h))
		{
			tree.v = true;
			continue;
		}

		// Check visibility above
		if (!trees.Exists(t => t.x == tree.x &&
							   t.y < tree.y &&
							   t.h >= tree.h))
		{
			tree.v = true;
			continue;
		}

		// Check visibility below
		if (!trees.Exists(t => t.x == tree.x &&
							   t.y > tree.y &&
							   t.h >= tree.h))
		{
			tree.v = true;
			continue;
		}

	}

	var count = trees.Where(t => t.v).Count();
	Console.WriteLine($"Part 1 Visible: {count}");

	var scenicScoreMax = 0;
	Console.WriteLine();
	var currentRow = 0;
	foreach (var tree in trees)
	{
		if (tree.y != currentRow)
		{
			Console.WriteLine();
			currentRow = tree.y;
		}
		Console.Write("^");
		// Check up
		foreach (var tree1 in trees.Where(t => t.x == tree.x && t.y < tree.y).OrderByDescending(t => t.y))
		{
			tree.su += 1;
			if (tree1.h >= tree.h)
			{
				break;
			}
		}

		// Check left
		foreach (var tree1 in trees.Where(t => t.y == tree.y && t.x < tree.x).OrderByDescending(t => t.x))
		{
			tree.sl += 1;
			if (tree1.h >= tree.h)
			{
				break;
			}
		}

		// Check right
		foreach (var tree1 in trees.Where(t => t.y == tree.y && t.x > tree.x).OrderBy(t => t.x))
		{
			tree.sr += 1;
			if (tree1.h >= tree.h)
			{
				break;
			}
		}

		// Check down
		foreach (var tree1 in trees.Where(t => t.x == tree.x && t.y > tree.y).OrderBy(t => t.y))
		{
			tree.sd += 1;
			if (tree1.h >= tree.h)
			{
				break;
			}
		}

		tree.ScenicScore = tree.su * tree.sl * tree.sr * tree.sd;
		if (tree.ScenicScore > scenicScoreMax)
		{
			scenicScoreMax = tree.ScenicScore;
		}
	}

	Console.WriteLine();
	Console.WriteLine($"Part 2 Scenic Score Max: {scenicScoreMax}");
}

// You can define other methods, fields, classes and namespaces here
public class Tree
{
	public int x;
	public int y;
	public int h;
	public bool v;
	public int su;
	public int sl;
	public int sr;
	public int sd;
	public int ScenicScore;
}