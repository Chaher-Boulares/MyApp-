using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class NotificationPermission:BaseModel
    {
        public NotificationPermission() { }
        public NotificationPermission(int uu, int entity, Boolean getNotif)
        {
            GetNotification = getNotif;
            UserId = uu;
            EntityId = entity;
        }

        //public int Id { get; set; }
        public int UserId { get; set; }
        public int EntityId { get; set; }
        public User user { get; set; }
        public Boolean GetNotification { get; set; }
    }
}
