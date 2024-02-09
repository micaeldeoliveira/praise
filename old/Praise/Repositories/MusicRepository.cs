using Microsoft.EntityFrameworkCore;
using Praise.Contexts;
using Praise.Models;
using Praise.Queries;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praise.Repositories
{
    public class MusicRepository
    {
        private readonly DataContext _context;

        public MusicRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MusicList>> GetMusicsAsync()
        {
            return await _context.Musics
                .AsNoTracking()
                .Select(x => new MusicList
                {
                    Id = x.Id,
                    Title = x.Title,
                    Reminder = x.Reminder,
                    Singer = x.Singer
                })
                .OrderBy(x => x.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<MusicList>> GetSearchMusicsAsync(string value)
        {
            var query = from e in _context.Musics
                        where EF.Functions.Like(e.Title, $"%{value}%") ||
                              EF.Functions.Like(e.Reminder, $"%{value}%") ||
                              EF.Functions.Like(e.Lirycs, $"%{value}%")
                        select e;
            return await query.AsNoTracking()
                .Select(x => new MusicList
                {
                    Id = x.Id,
                    Title = x.Title,
                    Singer = x.Singer
                })
                .OrderBy(x => x.Title).ToListAsync();
        }
        public async Task<Music> GetByIdAsync(int id)
        {
            return await _context.Musics.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Music model)
        {
            _context.Musics.Add(model);
        }

        public void Update(Music model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public void Delete(Music model)
        {
            _context.Musics.Remove(model);
        }
        

        public bool CheckExistsById(int id)
        {
            return _context.Musics.Any(x => x.Id == id);
        }
    }
}
