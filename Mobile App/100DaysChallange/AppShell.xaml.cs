using _100DaysChallange.Screens;

namespace _100DaysChallange;

public partial class AppShell : Shell
{
	public AppShell()
	{
		Routing.RegisterRoute(nameof(Home), typeof(Home));
		InitializeComponent();
	}
}
