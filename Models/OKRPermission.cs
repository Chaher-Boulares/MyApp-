using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class OKRPermission:BaseModel
    {
        public OKRPermission() { }
        public OKRPermission(Boolean add, Boolean upd, Boolean del)
        {
            Add = add;
            Update = upd;
            Delete = del;
        }
        public OKRPermission(int uu, int entity, Boolean add, Boolean upd, Boolean del, Boolean obs, Boolean addEmpl, Boolean delEmpl, Boolean addKR, Boolean delKR, Boolean updKR, Boolean obsKR)
        {
            userId = uu;
            EntityId = entity;
            Add = add;
            Update = upd;
            Delete = del;
            Observe = obs;
            AddEmployees = addEmpl;
            DeleteEmployees = delEmpl;
            AddKResult = addKR;
            DeleteKResult = delKR;
            updateKResult = updKR;
            ObserveKResult = obsKR;
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
        //public int okruserId { get; set; }
        //public User user { get; set; }
        public Boolean AddKResult { get; set; }
        public Boolean DeleteKResult { get; set; }
        public Boolean updateKResult { get; set; }
        public Boolean ObserveKResult { get; set; }
    }
}
