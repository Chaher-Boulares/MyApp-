using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class WallPermission:BaseModel
    {
        public WallPermission() { }
        public WallPermission(Boolean add, Boolean upd, Boolean del)
        {
            Add = add;
            Update = upd;
            Delete = del;
        }
        public WallPermission(int uu, int entity, Boolean add, Boolean upd, Boolean del, Boolean obs, Boolean addEmpl, Boolean delEmpl, Boolean display_FPQ_User)
        {
            userId = uu;
            EntityId = entity;
            Add = add;
            Update = upd;
            Delete = del;
            Observe = obs;
            AddEmployees = addEmpl;
            DeleteEmployees = delEmpl;
            DisplayFivePointQuestionUser = display_FPQ_User;
        }

        //public int Id { get; set; }
        public int userId { get; set; }
        public int EntityId { get; set; }
        public User user { get; set; }
        public Boolean Add { get; set; }
        public Boolean Update { get; set; }
        public Boolean Delete { get; set; }
        public Boolean Observe { get; set; }
        public Boolean AddEmployees { get; set; }
        public Boolean DeleteEmployees { get; set; }
        //public int walluserId { get; set; }
        //public User user { get; set; }
        public Boolean DisplayFivePointQuestionUser { get; set; }
    }
}
