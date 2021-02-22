using Masiv.RouletteProject.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Model.Entities
{ 
    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        public virtual T id { get; set; }
    }
}
