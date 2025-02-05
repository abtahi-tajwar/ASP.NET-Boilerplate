using System.Diagnostics;
using _100DaysChallange.Modules.Music.Entity;
using System.Reflection;
using System.Text.Json;

namespace _100DaysChallange.Modules.Music.Service;

public class MusicRepository
{
    public async Task<List<MusicEntity>> GetAllAsync()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("music.json");
        using var reader = new StreamReader(stream);
        string json = await reader.ReadToEndAsync();
        var Musics = JsonSerializer.Deserialize<List<MusicEntity>>(json);
        return Musics ?? new();
    }

    public async Task<MusicEntity?> GetMusicByIdAsync(int? id)
    {
        var _musics = await GetAllAsync();
        var music = _musics.FirstOrDefault(m => m.Id == id);
        return music;
    }   
}