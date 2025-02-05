using _100DaysChallange.Screens;
using _100DaysChallange.Screens.MusicDetails;

namespace _100DaysChallange;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("HomePage", typeof(Home));
		Routing.RegisterRoute(nameof(MusicDetails), typeof(MusicDetails));
	}
}
