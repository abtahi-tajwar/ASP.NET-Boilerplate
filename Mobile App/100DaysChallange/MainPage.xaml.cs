using _100DaysChallange.Screens;

namespace _100DaysChallange;

public partial class MainPage : ContentPage
{
	int count = 0;
	private MainPageViewModel _viewModel;

	public MainPage(MainPageViewModel model)
	{
		InitializeComponent();
		_viewModel = model;
		BindingContext = model;
	}

	protected override async void OnAppearing()
	{
		_viewModel.InitializeMusics();
	}
}

