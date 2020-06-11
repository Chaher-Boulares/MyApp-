using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Interfaces
{
    public interface INotificationPermissionRepository : IRepository<NotificationPermission>
    {
        public NotificationPermission GetByUserEntityId(int userId, int entityId);
    }
}
