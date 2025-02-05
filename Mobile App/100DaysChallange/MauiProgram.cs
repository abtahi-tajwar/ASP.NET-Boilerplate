using _100DaysChallange.Modules.Music.Service;
using _100DaysChallange.Screens;
using _100DaysChallange.Screens.MusicDetails;
using Microsoft.Extensions.Logging;

namespace _100DaysChallange;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		
		builder.Services.AddSingleton<MainPage>();
		
		#region MusicImports
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<MusicRepository>();
		builder.Services.AddTransient<MusicDetailsViewModel>();
		builder.Services.AddTransient<MusicDetails>();
		#endregion
		

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
