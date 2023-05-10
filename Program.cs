using TwitterTowers;

String ans;
BaseTower tower;
bool exit = false;
do
{
	Console.WriteLine ("Enter rectangle, triangle or exit");
	ans = Console.ReadLine ();
	ans = ans.ToLower ();

	switch (ans)
	{
		case "rectangle":
			tower = new Rectangle ();
			break;
		case "triangle":
			tower = new Triangle ();
			break;
		case "exit":
			exit = true;
			break;
		default:
			Console.WriteLine ("You have chosen a non -existing option");
			break;
	}
} while (!exit);
