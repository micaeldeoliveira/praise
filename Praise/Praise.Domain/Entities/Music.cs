namespace Praise.Domain.Entities;

public class Music : Entity
{
    public Music(string title, string? reminder, string? singer, string? lirycs, string? video, bool play)
    {
        Title = title.Trim();
        Reminder = reminder?.Trim();
        Singer = singer?.Trim();
        Lirycs = lirycs?.Trim();
        Video = video?.Trim();
        Play = play;
    }

    public string Title { get; private set; }
    public string? Reminder { get; private set; }
    public string? Singer { get; private set; }
    public string? Lirycs { get; private set; }
    public string? Video { get; private set; }
    public bool Play { get; private set; }

    public void Update(string title, string? reminder, string? singer, string? lirycs, string? video, bool play)
    {
        Title = title.Trim();
        Reminder = reminder?.Trim();
        Singer = singer?.Trim();
        Lirycs = lirycs?.Trim();
        Video = video?.Trim();
        Play = play;
    }
}