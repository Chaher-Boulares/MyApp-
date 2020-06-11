using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly OrganizationDBContext _context;
        public UserRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }

        public User GetByUserId(string userid)
        {
            return _context.User.Where(i => i.UserId == userid).FirstOrDefault();
        }
    }
}
