using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class User:BaseModel
    {
        public User() { }
        public User(string usrname, string email, string userId)
        {
            UserName = usrname;
            Email = email;
            UserId = userId;
        }

        //public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        //public string Role { get; set; }
        //public AffectedPermissions wall_okrPermission { get; set; }
        public ICollection<WallPermission> wallPermission { get; set; }
        public ICollection<OKRPermission> okrPermission { get; set; }
        public ICollection<OrganizationPermission> organizationPermission { get; set; }
        public ICollection<NotificationPermission> notifPermission { get; set; }
        public ICollection<User_Entity> UserEntitees { get; set; }
        //public IList<User_Role> RolesUser { get; set; }
        //public List<IDictionary<int, Role>> RolesParEntity { get; set; }
    }
}
