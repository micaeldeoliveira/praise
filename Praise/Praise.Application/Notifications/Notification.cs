using Praise.Application.Interfaces.Notifications;

namespace Praise.Application.Notifications;

public class Notification : INotification
{
    private Dictionary<string, string> _errors = new();

    public bool Has() => _errors.Any();

    public void Add(string key, string value) => _errors.Add(key, value);

    public Dictionary<string, string> Notifications() => _errors;

}
