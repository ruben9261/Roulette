using Entity = Masiv.RouletteProject.Model.Entities;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Masiv.RouletteProject.Model.Constants;
using Newtonsoft.Json;
using Masiv.RouletteProject.Repository.Implementation;
using Masiv.RouletteProject.Repository.Contract;

namespace Masiv.RouletteProject.Repository.Implementation
{
    public class UserRepository : BaseRepository<Entity.User>, IUserRepository
    {        
        public UserRepository(IConnectionMultiplexer IConnectionMultiplexer)
            : base(IConnectionMultiplexer, KeyConstants.ROULETTE_KEY)
        {

        }
    }
}
