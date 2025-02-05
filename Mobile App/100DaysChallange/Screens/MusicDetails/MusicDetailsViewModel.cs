using _100DaysChallange.Factory;
using _100DaysChallange.Modules.Music.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using _100DaysChallange.Modules.Music.Entity;
using _100DaysChallange.Modules.Music.Service;

namespace _100DaysChallange.Screens.MusicDetails;

[QueryProperty(nameof(MusicId), nameof(MusicId))]
public partial class MusicDetailsViewModel : BaseViewModel
{
    private MusicRepository _musicRepository;
    public MusicDetailsViewModel(MusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    [ObservableProperty] private string musicId;
    [ObservableProperty] private MusicEntity? music;

    partial void OnMusicIdChanged(string value)
    {
        LoadMusicAsync(value).ConfigureAwait(false);
    }
    
    private async Task LoadMusicAsync(string value)
    {
        if (int.TryParse(value, out int musicId))
        {
            Music = await _musicRepository.GetMusicByIdAsync(musicId);
        }
        else
        {
            Music = null;
        }
    }
}