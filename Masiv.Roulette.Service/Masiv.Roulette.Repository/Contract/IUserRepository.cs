using System;
using System.Collections.Generic;
using System.Text;
using Entity = Masiv.RouletteProject.Model.Entities;

namespace Masiv.RouletteProject.Repository.Contract
{
    public interface IUserRepository : IBaseRepository<Entity.User>
    {
    }
}
