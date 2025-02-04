using CommunityToolkit.Mvvm.ComponentModel;

namespace _100DaysChallange.Factory;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    string title = "";
    [ObservableProperty]
    bool isBusy = false;
}
