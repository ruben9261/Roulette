using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Base
{
    public interface IBaseEntity<T>
    {
        T id { get; set; }
    }
}
