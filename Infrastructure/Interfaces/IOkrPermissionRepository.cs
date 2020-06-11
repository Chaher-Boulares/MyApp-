using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Interfaces
{
    public interface IOkrPermissionRepository : IRepository<OKRPermission>
    {
        public OKRPermission GetByUserEntityId(int userId, int entityId);
    }
}
