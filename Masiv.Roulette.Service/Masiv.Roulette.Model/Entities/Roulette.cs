using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Entities
{
    public class Roulette: BaseEntity<String>
    {
        public Boolean status { get; set; }
        public List<Wager> lstWager { get; set; }
    }
}
