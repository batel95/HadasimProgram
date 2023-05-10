namespace TwitterTowers
{
	internal class Triangle : BaseTower
	{
		private int iHeight;
		private int iWidth;

		public Triangle ()
		{
			iHeight = (int) Height;
			iWidth = (int) Width;
			Choose ();
		}

		private void Choose ()
		{
			string ans;
			Console.WriteLine ("Enter perimeter to calculate the triangle perimeter, Enter print to print the triangle.");
			do
			{
				ans = Console.ReadLine ();
				ans = ans.ToLower ();

				switch (ans)
				{
					case "perimeter":
						perimeter (hypotenuse (Height, Width / 2), Width);
						break;

					case "print":
						if (!Printable ())
							Console.WriteLine ("Cannot print the triangle");
						else
							triangleStr ();
						break;

					default:
						Console.WriteLine ("You have chosen a non -existing option");
						ans = "";
						break;
				}
			} while (ans == "");
		}

		private void triangleStr ()
		{
			var starArr = new int[iHeight];
			var stars = iWidth;
			var s = (iHeight - 2)/(((iWidth - 1) / 2) - 1);
			string tempStr = "";
			int j = 0;
			starArr [0] = 1;
			starArr [starArr.Length - 1] = stars;
			stars -= 2;

			for (int i = starArr.Length - 2; i > 0; i--)
			{
				if (j == s)
				{
					if (stars != 3)
					{
						stars -= 2;
						j = 0;
					}
				}
				starArr [i] = stars;
				j++;
			}

			for (int i = 0; i < starArr.Length; i++)
			{
				tempStr = "";
				for (int k = 0; k < starArr [i]; k++)
				{
					tempStr += "*";
				}
				Console.WriteLine (tempStr.PadLeft (((iWidth - starArr [i]) / 2) + starArr [i]));
			}

		}

		private Func<double, double, double> hypotenuse = (legA, legB)
			=> Math.Sqrt(Math.Pow(legA, 2) + Math.Pow(legB, 2));


		private Action<double, double> perimeter = (tSide, tBase)
			=> Console.WriteLine("Perimeter is: " + (2 * tSide + tBase));

		private bool Printable ()
			=> (iHeight == Height && iWidth == Width && iWidth % 2 != 0 && iWidth <= 2 * iHeight);

	}
}
