using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.ViewModels
{
    public class InviteViewModel
    {
        public int InviterId { get; set; }
        public int NewMemberId { get; set; }
        public int EntityId { get; set; }
        public Role NewRole { get; set; }
    }
}
