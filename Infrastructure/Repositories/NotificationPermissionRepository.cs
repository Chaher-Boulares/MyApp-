using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class NotificationPermissionRepository : BaseRepository<NotificationPermission>, INotificationPermissionRepository
    {
        private readonly OrganizationDBContext _context;
        public NotificationPermissionRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }

        public NotificationPermission GetByUserEntityId(int userId, int entityId)
        {
            return _context.NotificationPermission.Where(i => i.UserId == userId).Where(b => b.EntityId == entityId).FirstOrDefault();
        }
    }
}
