using Praise.Application.Interfaces.Repositories;
using Praise.Domain.Entities;
using Praise.Infra.Contexts;

namespace Praise.Infra.Repositories;


public class MusicRepository : Repository<Music>, IMusicRepository
{
    public MusicRepository(AppDbContext context) : base(context)
    {
    }
}
