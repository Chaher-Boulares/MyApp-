using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.ViewModels
{
    public class UpdateOraganizationSettings
    {
        public int UserId { get; set; }
        public int EntityId { get; set; }
        public Boolean ChangeSettings { get; set; }
        public Boolean ChangeOrganizationProfile { get; set; }
        public Boolean AddEntities { get; set; }
    }
}
