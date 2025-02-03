using System.Windows.Input;

namespace _100DaysChallange.Screens;
using CommunityToolkit.Mvvm.Input;

public class MainPageViewModel
{
    public ICommand ButtonCommand { get; }

    public MainPageViewModel () {
        ButtonCommand = new RelayCommand(OnHomeButtonClick);
    }
    async void OnHomeButtonClick()
    {
        await Shell.Current.GoToAsync(nameof(Home));
    }
}
