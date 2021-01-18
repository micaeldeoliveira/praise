using Microsoft.EntityFrameworkCore;
using Praise.Contexts;
using Praise.Models;
using Praise.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praise.Repositories
{
    public class UserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserList>> GetAllAsync()
        {
            return await _context.Users
                .Select(x => new UserList 
                {
                   Id = x.Id,
                   Name = x.Name,
                   Email = x.Email,
                   Phone = x.Phone,
                   Photo = x.Photo
                })
                .OrderBy(x => x.Name)
                .ToListAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users                
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool CheckExistsById(int id)
        {
            return _context.Users.Any(x => x.Id == id);
        }
    }
}
