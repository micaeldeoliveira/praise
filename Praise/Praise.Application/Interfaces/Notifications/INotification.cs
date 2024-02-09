namespace Praise.Application.Interfaces.Notifications;

public interface INotification
{
    bool Has();
    void Add(string key, string value);
    Dictionary<string, string> Notifications();
}