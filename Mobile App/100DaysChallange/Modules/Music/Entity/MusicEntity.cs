using System.Text.Json.Serialization;

namespace _100DaysChallange.Modules.Music.Entity;

public class MusicEntity
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("artist")]
    public string Artist { get; set; }
    
    [JsonPropertyName("duration")]
    public string Duration { get; set; }

    // Optional: Override ToString() for easy debugging
    public override string ToString()
    {
        return $"{Title} - {Artist} ({Duration})";
    }
}
