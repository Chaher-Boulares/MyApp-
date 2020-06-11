using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Interfaces
{
    public interface IOrganizationPermissionRepository : IRepository<OrganizationPermission>
    {
        public OrganizationPermission GetOrgPermission(int userid, int entityid);
    }
}
