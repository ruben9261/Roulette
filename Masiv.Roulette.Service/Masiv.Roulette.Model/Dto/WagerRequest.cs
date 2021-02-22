using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Dto
{
    public class WagerRequest
    {
        public decimal money { get; set; }
        public String color { get; set; }
        public int? number { get; set; }
    }
}
