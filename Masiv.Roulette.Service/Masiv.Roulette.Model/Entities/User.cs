using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Entities
{
    public class User: BaseEntity<String>
    {        
        public String name { get; set; }
        public decimal balance { get; set; }
    }
}
