using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Interfaces
{
    public interface IUser_EntityRepository : IRepository<User_Entity>
    {
        public User_Entity GetByUserId(int userid, int entityid);
        public void Remove(int entityid);
        public List<User_Entity> GetAdminsOfEntity(int entityid);
    }
}
