using Praise.Core.Requests.Musics;
using Praise.Domain.Entities;

namespace Praise.Application.Interfaces.Services;

public interface IMusicService
{
    Task<Music?> AddAsync(MusicRequest request);
    Task<Music?> UpdateAsync(MusicRequest request);
}