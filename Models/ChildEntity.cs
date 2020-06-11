using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class ChildEntity:BaseModel
    {
        public ChildEntity() { }
        public ChildEntity(Entity parent, Entity child)
        {
            ParentId = parent.Id;
            ChildId = child.Id;
        }
       // public int Id { get; set; }
        public int ParentId { get; set; }
        public int ChildId { get; set; }
    }
}
