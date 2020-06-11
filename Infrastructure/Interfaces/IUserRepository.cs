using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure.Interfaces
{
    public  interface IUserRepository : IRepository<User>
    {
        public User GetByUserId(string userid);
    }
}
