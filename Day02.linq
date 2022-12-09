<Query Kind="Program" />

void Main()
{
	var input = File.ReadAllLines(@"c:\AdventOfCode\2022\Day02_1.txt");
	var scores = new List<RPSRound>();
	foreach (var line in input)
	{
		Console.WriteLine(line);
		var scoreTemp = new RPSRound(line.Substring(0, 1).ToCharArray()[0], line.Substring(2, 1).ToCharArray()[0]);
		scores.Add(scoreTemp);
		Console.WriteLine($"Score: {scoreTemp.Score}");
		Console.WriteLine($"Score Alternate:  {scoreTemp.Score2}");
	}

	Console.WriteLine($"Score: {scores.Sum(i => i.Score)}");
	Console.WriteLine($"Score Alternate: {scores.Sum(i => i.Score2)}");
}

// You can define other methods, fields, classes and namespaces here
public class RPSRound
{
	public char OpponentHand { get; set; }
	public char YourHand { get; set; }
	public int Score { get; set; }
	public int Score2 { get; set; }

	public RPSRound(char opponentHand, char yourHand)
	{
		OpponentHand = opponentHand;
		YourHand = yourHand;
		switch (OpponentHand)
		{
			case 'A':
				switch (YourHand)
				{
					case 'X':
						//Draw
						Score = 1 + 3;
						break;
					case 'Y':
						//Win
						Score = 2 + 6;
						break;
					case 'Z':
						//Lose
						Score = 3;
						break;
				}
				break;
			case 'B':
				switch (YourHand)
				{
					case 'X':
						//Lose
						Score = 1;
						break;
					case 'Y':
						//Draw
						Score = 2 + 3;
						break;
					case 'Z':
						//Win
						Score = 3 + 6;
						break;
				}
				break;
			case 'C':
				switch (YourHand)
				{
					case 'X':
						//Win
						Score = 1 + 6;
						break;
					case 'Y':
						//Lose
						Score = 2;
						break;
					case 'Z':
						//Draw
						Score = 3 + 3;
						break;
				}
				break;
			default:
				Score = 0;
				break;
		}

		switch (OpponentHand)
		{
			case 'A': // Rock
				switch (YourHand)
				{
					case 'X':
						//Lose Scissors
						Score2 = 3;
						break;
					case 'Y':
						//Draw Rock
						Score2 = 1 + 3;
						break;
					case 'Z':
						//Win Paper
						Score2 = 2 + 6;
						break;
				}
				break;
			case 'B': // Paper
				switch (YourHand)
				{
					case 'X':
						//Lose Rock
						Score2 = 1;
						break;
					case 'Y':
						//Draw Paper
						Score2 = 2 + 3;
						break;
					case 'Z':
						//Win Scissors
						Score2 = 3 + 6;
						break;
				}
				break;
			case 'C': // Scissors
				switch (YourHand)
				{
					case 'X':
						//Lose Paper
						Score2 = 2;
						break;
					case 'Y':
						//Draw Scissors
						Score2 = 3 + 3;
						break;
					case 'Z':
						//Win Rock
						Score2 = 1 + 6;
						break;
				}
				break;
			default:
				Score2 = 0;
				break;
		}

	}
}