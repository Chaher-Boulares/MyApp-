using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class User_EntityRepository : BaseRepository<User_Entity>, IUser_EntityRepository
    {
        private readonly OrganizationDBContext _context;
        public User_EntityRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }

        public User_Entity GetByUserId(int userid, int entityid)
        {
            return _context.User_Entity.Where(i => i.UserId == userid).Where(a => a.EntityId == entityid).FirstOrDefault();
        }
        public  void Remove(int entityid)
        {
            var userentity = _context.User_Entity.Where(x =>x.Id ==entityid).FirstOrDefault();
            _context.User_Entity.Remove(userentity);
        }
        public List<User_Entity> GetAdminsOfEntity(int entityid)
        {
            return _context.User_Entity.Where(a => a.EntityId == entityid).Where(b => b.Role == 0).ToList<User_Entity>();
        }
    }
}
