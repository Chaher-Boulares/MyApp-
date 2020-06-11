using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class OrganizationPermissionRepository : BaseRepository<OrganizationPermission>, IOrganizationPermissionRepository
    {
        private readonly OrganizationDBContext _context;
        public OrganizationPermissionRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }
        public OrganizationPermission GetOrgPermission(int userid, int entityid)
        {
            return _context.OrganizationPermission.Where(i => i.UserId == userid).Where(a => a.EntityId == entityid).FirstOrDefault();
        }
    }
}
