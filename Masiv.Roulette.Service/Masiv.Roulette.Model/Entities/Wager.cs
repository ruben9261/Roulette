using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Entities
{
    public class Wager
    {
        public String value { get; set; }
        public decimal cash { get; set; }
        public User user { get; set; }
    }
}
