using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class OkrPermissionRepository : BaseRepository<OKRPermission>, IOkrPermissionRepository
    {
        private readonly OrganizationDBContext _context;
        public OkrPermissionRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }

        public OKRPermission GetByUserEntityId(int userId, int entityId)
        {
            return _context.OKRPermission.Where(i => i.userId == userId).Where(b => b.EntityId == entityId).FirstOrDefault();
        }
    }
}
