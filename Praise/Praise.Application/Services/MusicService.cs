using Praise.Application.Interfaces.Notifications;
using Praise.Application.Interfaces.Repositories;
using Praise.Application.Interfaces.Services;
using Praise.Core.Requests.Musics;
using Praise.Domain.Entities;

namespace Praise.Application.Services;

public class MusicService(
    INotification _notification,
    IMusicRepository _musicRepository)
    : IMusicService
{
    public async Task<Music?> AddAsync(MusicRequest request)
    {
        var music = new Music(request.Title, request.Reminder,
            request.Singer, request.Lirycs, request.Video, request.Play);

        //TODO validation

        await _musicRepository.AddAsync(music);

        await _musicRepository.SaveChangesAsync();

        return music;

    }

    public async Task<Music?> UpdateAsync(MusicRequest request)
    {
        var music = await _musicRepository.GetByIdAsync(request.Id);

        if (music == null)
        {
            _notification.Add("music", "A música não foi encontrada.");
            return null;
        }

        music.Update(request.Title, request.Reminder,
            request.Singer, request.Lirycs, request.Video, request.Play);

        _musicRepository.Update(music);

        await _musicRepository.SaveChangesAsync();

        return music;
    }
}
