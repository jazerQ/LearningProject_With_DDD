using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions.ForRepositories;
using Core.Enums;
using Core.Models;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LearningCoursesDbContext _context;
        public UserRepository(LearningCoursesDbContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
            var userEntity = new UserEntity() { Id = user.Id, Username = user.Username, PasswordHash = user.PasswordHash, Email = user.Email };
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users.AsNoTracking()
                                                 .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
            var user = User.Create(userEntity.Id, userEntity.Username, userEntity.PasswordHash, userEntity.Email);
            if (user.IsFailure)
            {
                throw new Exception(user.Error);
            }
            return user.Value;

        }
        public async Task<HashSet<Permission>> GetUserPermissions(Guid id) 
        {
            var roles = await _context.Users
                            .AsNoTracking()
                            .Include(u => u.Roles)
                            .Where(u => u.Id == id)
                            .Select(u => u.Roles)
                            .ToArrayAsync();
            return roles.SelectMany(r => r)
                        .SelectMany(r => r.Permissions)
                        .Select(r => (Permission)r.Id)
                        .ToHashSet();
           

        }
    }
}
