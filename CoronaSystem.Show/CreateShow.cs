using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSystem.Show
{
	public class CreateShow
	{
		public BaseShow Show { get; private set; }


	}


	public class BaseShow
	{

	}

	public class UserImageShow : BaseShow
	{
		public String Uri { get; private set; }
	}

	public class CoronaSumShow : BaseShow
	{

	}
}
