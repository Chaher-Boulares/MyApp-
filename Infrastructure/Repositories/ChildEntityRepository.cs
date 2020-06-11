using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Repositories
{
    public class ChildEntityRepository : BaseRepository<ChildEntity>, IChildEntityRepository
    {
        private readonly OrganizationDBContext _context;
        public ChildEntityRepository(OrganizationDBContext dbContext)
        : base(dbContext)
        {
            _context = dbContext;
        }
        public List<ChildEntity> GetByParentId(int id)
        {
            return _context.ChildEntities.Where(i => i.ParentId == id).ToList<ChildEntity>();
        }
    }
}
