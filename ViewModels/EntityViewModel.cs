using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.ViewModels
{
    public class EntityViewModel
    {
        public string Name { get; set; }   
        public string Description { get; set; } 
        public string adresse { get; set; }
        public int creator { get; set; }
        public int ParentEntityId { get; set;}
    }
}
