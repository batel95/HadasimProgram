using CoronaSystem.Show.ViewModels;
using CoronaSystem.Show.Views;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoronaSystem.Show
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup (StartupEventArgs e)
		{
			MainWindow window = new MainWindow();
			window.Show ();
			/*window.DataContext = new CoronaSumViewModel();
			window.Show ();
			MainWindow window1 = new MainWindow();
			window1.DataContext = new UserImageViewModel ();
			window1.Show ();*/
		}
	}
}
