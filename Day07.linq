<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day07_1.txt");
	var fullPath = "";
	var path = new Stack<string>();
	var currentDirectory = "";
	var directories = new Dictionary<string, int>();
	foreach (var line in input)
	{
		//Console.WriteLine(line);
		var lineStart = line.Substring(0, 1);
		if (lineStart == "$")
		{
			//Console.WriteLine("Command");
			var command = line.Split(" ");
			if (command[1] == "cd")
			{
				//Console.WriteLine($"Change Directory to {command[2]}");
				switch (command[2])
				{
					case "/":
						path.Clear();
						path.Push(command[2]);
						fullPath = "/";
						currentDirectory = "/";
						if (!directories.ContainsKey(command[2])) directories.Add(command[2], 0);
						break;
					case "..":
						var temp = path.Pop();
						fullPath = "";
						foreach (var s in path.Reverse().ToArray())
						{
							if (s != "/")
								fullPath = fullPath + "/" + s;
						}
						//Console.WriteLine($"Current Path: {fullPath}");
						break;
					default:
						path.Push(command[2]);
						fullPath = "";
						foreach (var s in path.Reverse().ToArray())
						{
							if (s != "/")
								fullPath = fullPath + "/" + s;
						}
						//Console.WriteLine($"Current Path: {fullPath}");
						if (!directories.ContainsKey(fullPath)) directories.Add(fullPath, 0);
						break;
				}
			}
			else if (command[1] == "ls")
			{
				//Console.WriteLine("List");
			}
		}
		else if (lineStart == "d")
		{
			//Console.WriteLine("Directory");
		}
		else if (lineStart is "0" or "1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9")
		{
			//Console.WriteLine("File");
			var split = line.Split(' ');
			// add to directory
			//directories[fullPath] += Convert.ToInt32(split[0]);
			// add to parent directories
			var tempPath = "";
			foreach (var s in path.Reverse().ToArray())
			{
				if (s != "/")
				{
					tempPath = tempPath + "/" + s;
					directories[tempPath] += Convert.ToInt32(split[0]);
				}
				else
				{
					directories["/"] += Convert.ToInt32(split[0]);
				}

			}
		}

	}

	foreach (var directory in directories)
	{
		//Console.WriteLine($"directory: {directory.Key} size: {directory.Value}");
	}

	var result = directories.Where(kvp => kvp.Value <= 100000).Sum(kvp => kvp.Value);
	Console.WriteLine($"Part 1: {result}");

	var result2 = 0;
	var totalSpace = 70000000;
	var neededSpace = 30000000;
	var currentAvailableSpace = totalSpace - directories["/"];
	var spaceNeededToBeFreedUp = neededSpace - currentAvailableSpace;
	Console.WriteLine($"Current available: {currentAvailableSpace}");
	Console.WriteLine($"Needed: {spaceNeededToBeFreedUp}");

	var sortedDirectories = directories.OrderBy(x => x.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
	result2 = sortedDirectories.First(kvp => kvp.Value > spaceNeededToBeFreedUp).Value;

	Console.WriteLine($"Part 2: {result2}");
}

// You can define other methods, fields, classes and namespaces here