using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class User_Entity:BaseModel
    {
        public User_Entity() { }
        public User_Entity(User usr, Entity entity)
        {
            UserId = usr.Id;
            EntityId = entity.Id;
            user = usr;
            Entity = entity;
        }

        public User_Entity(User usr, Entity entity, Role role)
        {
            UserId = usr.Id;
            EntityId = entity.Id;
            user = usr;
            Entity = entity;
            Role = role;
        }

        //public int Id { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int EntityId { get; set; }
        public Entity Entity { get; set; }
        public Role Role { get; set; }
    }
}
