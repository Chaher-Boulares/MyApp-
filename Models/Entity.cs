using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Models
{
    public class Entity:BaseModel
    {
        public Entity() { }
        public Entity(string name, string descr, string adres)
        {
            Name = name;
            Description = descr;
            Address = adres;
        }

        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Weekend { get; set; }
        public string OkrDisplay { get; set; }
        public string OkrStatus { get; set; }
        public string WallDisplayed { get; set; }
        public string EntityDisplayed { get; set; }
        public byte Avatar { get; set; }
        public ICollection<User_Entity> Employees { get; set; }
        //public ICollection<ChildEntity> ChildEntity { get; set; } 
        //public Entity Ent { get; set; }
        public int Parent_id { get; set; }
        public DateTime TimeZone { get; set; }
    }
}
