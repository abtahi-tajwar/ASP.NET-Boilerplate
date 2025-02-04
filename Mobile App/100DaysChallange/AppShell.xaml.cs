using _100DaysChallange.Screens;

namespace _100DaysChallange;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("HomePage", typeof(Home));
	}
}
