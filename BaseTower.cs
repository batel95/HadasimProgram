namespace TwitterTowers
{
	internal class BaseTower
	{
		protected double Height { get; set; }
		protected double Width { get; private set; }

		private const int MIN_HEIGHT = 2;
		private const int MIN_WIDTH = 0;

		protected BaseTower ()
		{
			double input;
			Console.Write ("Enter tower height: ");
			input = double.Parse (Console.ReadLine ());
			if (input >= MIN_HEIGHT)
			{
				Height = input;
				Console.Write ("Enter tower width: ");
				input = double.Parse (Console.ReadLine ());
				if (input > MIN_WIDTH)
				{
					Width = input;
				}
				else
					Console.WriteLine ("Invalid value. Tower width must be greater than {0}", MIN_WIDTH);
			}
			else
				Console.WriteLine ("Invalid value. Tower width must be greater than or equal to {0}", MIN_HEIGHT);
		}
	}
}
