using TwitterTowers;

String ans;
BaseTowers tower;
bool exit = false;
do
{
	Console.WriteLine ("Enter rectangle, triangular or exit");
	ans = Console.ReadLine ();
	ans = ans.ToLower ();

	switch (ans)
	{
		case "rectangle":
			tower = new Rectangle ();
			break;
		case "triangular":
			tower = new Triangular ();
			break;
		case "exit":
			exit = true;
			break;
		default:
			Console.WriteLine ("You have chosen a non -existing option");
			break;
	}
} while (!exit);
