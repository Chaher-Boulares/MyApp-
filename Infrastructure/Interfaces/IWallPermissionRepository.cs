using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Interfaces
{
    public interface IWallPermissionRepository : IRepository<WallPermission>
    {
        public WallPermission GetByUserEntityId(int userId, int entityId);
    }
}
