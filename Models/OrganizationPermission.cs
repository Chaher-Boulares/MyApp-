using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class OrganizationPermission:BaseModel
    {
        public OrganizationPermission() { }
        public OrganizationPermission(int uu, int entity, Boolean addEn, Boolean chSet, Boolean chPrf)
        {
            AddEntities = addEn;
            ChangeSettings = chSet;
            ChangeOrganizationProfile = chPrf;
            EntityId = entity;
            UserId = uu;
        }

        //public int Id { get; set; }
        public int UserId { get; set; }
        public int EntityId { get; set; }
        public User user { get; set; }
        public Boolean ChangeSettings { get; set; }
        public Boolean ChangeOrganizationProfile { get; set; }
        public Boolean AddEntities { get; set; }
    }
}
