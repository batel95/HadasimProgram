using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaSystem.Show.ViewModels
{
	internal class UserImageViewModel
	{
		public UserImageViewModel() 
		{
			ImageUri = Path.Combine (Environment.CurrentDirectory, @"\Images\DefaultUserImage.jpg");
		}

		public String ImageUri { get; private set; } 
	}
}