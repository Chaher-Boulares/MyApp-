using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class WallPermissionRepository : BaseRepository<WallPermission>, IWallPermissionRepository
    {
        private readonly OrganizationDBContext _context;
        public WallPermissionRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }

        public WallPermission GetByUserEntityId(int userId ,int entityId)
        {
            return _context.WallPermission.Where(i => i.userId == userId).Where(b => b.EntityId == entityId).FirstOrDefault();
        }
    }
}
