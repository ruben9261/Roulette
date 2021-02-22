using Masiv.RouletteProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Dto
{
    public class WagerResponse
    {
        public int resultNumber { get; set; }
        public String resultColor { get; set; }
        public int totalLosers { get; set; }
        public int totalWinners { get; set; }
        public List<Wager> lstWagerWinners { get; set; }
        public List<Wager> lstWagerLosers { get; set; }
    }
}
