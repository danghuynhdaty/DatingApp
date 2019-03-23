using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Api.Helpers;
using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoAsync(int userId)
        {
            return await _context.Photos.FirstOrDefaultAsync(p => p.UserId == userId && p.IsMain);
        }

        public async Task<Photo> GetPhotoAsync(int id)
        {
            return await _context.Photos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PagedList<User>> GetUsersAsync(UserParams userParams)
        {
            var users = _context.Users.Include(u => u.Photos)
                .OrderByDescending(u => u.LastActive)
                .Where(u => u.Id != userParams.UserId && u.Gender == userParams.Gender);

            if (userParams.MaxAge != 99 || userParams.MinAge != 18)
            {
                // minimum date of birth
                var minDob = DateTime.Now.AddYears(-userParams.MaxAge - 1);
                // maximum date of birth
                var maxDob = DateTime.Now.AddYears(-userParams.MinAge);
                users = users.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}