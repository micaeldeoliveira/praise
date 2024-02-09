using Microsoft.EntityFrameworkCore;
using Praise.Contexts;
using Praise.Models;
using Praise.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praise.Repositories
{
    public class EventRepository
    {
        private readonly DataContext _context;
        public EventRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events
                         .AsNoTracking()
                         .OrderBy(a => a.Date)
                         .ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<EventUser>> GetEventUserByIdAsync(int eventId)
        {
            return await _context.EventsUsers.AsNoTracking().Where(x => x.EventId == eventId).ToListAsync();
        }

        public async Task<IEnumerable<EventMusic>> GetEventMusicByIdAsync(int eventId)
        {
            return await _context.EventsMusics.AsNoTracking().Where(x => x.EventId == eventId).ToListAsync();            
        }

        public async Task<IEnumerable<int>> GetEventUserIdByIdAsync(int eventId)
        {
            return await _context.EventsUsers.AsNoTracking()
                .Where(x => x.EventId == eventId)
                .Select(x => x.UserId)
                .ToListAsync();
        }
        public async Task<IEnumerable<int>> GetEventMusicIdByIdAsync(int eventId)
        {
            return await _context.EventsMusics.AsNoTracking()
                .Where(x => x.EventId == eventId)
                .Select(x => x.MusicId)
                .ToListAsync();
        }

        public async Task<EventDetailsQuery> GetByIdViewDetailsAsync(int id)
        {
            var eventDetails = await _context.Events.
                Select(x => new EventDetailsQuery
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    Date = x.Date,
                    EndHour = x.EndHour,
                    LastModifiedDate = x.LastModifiedDate,
                    Local = x.Local,
                    Name = x.Name,
                    Note = x.Note,
                    StartHour = x.StartHour,
                    Status = x.Status
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            eventDetails.Users = await GetEventUsersAsync(id);
            eventDetails.Musics = await GetEventMusicsAsync(id);

            return eventDetails;
        }
        public async Task<IEnumerable<UserList>> GetEventUsersAsync(int eventId)
        {
            return await _context.EventsUsers
                    .AsNoTracking()
                    .Include(e => e.User)
                    .OrderBy(e => e.User.Name)
                    .Where(e => e.EventId == eventId)
                    .Select(x => new UserList
                    {
                        Id = x.User.Id,
                        Name = x.User.Name,
                        Email = x.User.Email,
                        Phone = x.User.Phone,
                        Photo = x.User.Photo
                    })
                    .ToListAsync();
        }
        public async Task<IEnumerable<MusicList>> GetEventMusicsAsync(int eventId)
        {
            return await _context.EventsMusics
                    .AsNoTracking()
                    .Include(e => e.Music)
                    .OrderBy(e => e.Order)
                    .Where(e => e.EventId == eventId)
                                    .Select(x => new MusicList
                                    {
                                        Id = x.Music.Id,
                                        Title = x.Music.Title,
                                        Singer = x.Music.Singer
                                    })
                    .ToListAsync();
        }
        
        public void Add(Event model)
        {
            _context.Events.Add(model);
        }
        public void AddEventUser(EventUser model)
        {
            _context.EventsUsers.Add(model);
        }
        public void AddEventMusic(EventMusic model)
        {
            _context.EventsMusics.Add(model);
        }
        public void Update(Event model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
        public bool CheckExistsById(int id)
        {
            return _context.Events.Any(x => x.Id == id);
        }
        public bool CheckUserAlreadyAdded(int eventId, int userId)
        {
            return _context.EventsUsers.Any(x => x.EventId == eventId && x.UserId == userId);
        }
        public bool CheckMusicAlreadyAdded(int eventId, int musicId)
        {
            return _context.EventsMusics.Any(x => x.EventId == eventId && x.MusicId == musicId);
        }
        public async Task<EventMusic> GetEventMusicById(int eventId, int musicId)
        {
            return await _context.EventsMusics.FirstOrDefaultAsync(x => x.EventId == eventId && x.MusicId == musicId);
        }
        public void UpdateEventMusic(EventMusic model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
       
        public void DeleteEvent(int eventId)
        {
            _context.EventsUsers.RemoveRange(new EventUser { EventId = eventId });
            _context.EventsMusics.RemoveRange(new EventMusic { EventId = eventId });
            //_context.Events.Remove(new Event)

        }
        
        public void DeleteUser(int eventId, int userId)
        {
            _context.EventsUsers.Remove(new EventUser
            {
                EventId = eventId,
                UserId = userId
            });
        }
        public void DeleteMusic(int eventId, int musicId)
        {
            _context.EventsMusics.Remove(new EventMusic
            {
                EventId = eventId,
                MusicId = musicId
            });
        }

    }
}