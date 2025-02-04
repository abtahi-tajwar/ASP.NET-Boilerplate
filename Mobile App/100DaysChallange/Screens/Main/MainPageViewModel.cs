using System.Collections.ObjectModel;
using System.Windows.Input;
using _100DaysChallange.Modules.Music.Entity;
using _100DaysChallange.Modules.Music.Service;

namespace _100DaysChallange.Screens;

using _100DaysChallange.Factory;
using CommunityToolkit.Mvvm.Input;

public partial class MainPageViewModel : BaseViewModel
{
    private MusicRepository _repository;
    public ObservableCollection<MusicEntity> Musics { get; } = new();
    
    public MainPageViewModel(MusicRepository repository)
    {
        _repository = repository;
    }
    
    public async Task InitializeMusics()
    {
        var _musics = await _repository.GetAllAsync();
        
        foreach (var m in _musics)
        {
            Musics.Add(m);
        }
    }

    [RelayCommand]
    async Task OnHomeButtonClick()
    {
        await Shell.Current.GoToAsync("HomePage");
    }
    
}
