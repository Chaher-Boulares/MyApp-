using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class EntityRepository : BaseRepository<Entity>, IEntityRepository
    {
        public EntityRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {

        }
    }
}
