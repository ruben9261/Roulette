using Masiv.RouletteProject.Model.Constants;
using Masiv.RouletteProject.Model.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Repository.Implementation
{
    public class BaseRepository<TEntity>: IDisposable where TEntity : BaseEntity<String>, new()
    {
        protected readonly IConnectionMultiplexer _IConnectionMultiplexer;
        protected readonly String _redisKey;

        public BaseRepository(IConnectionMultiplexer IConnectionMultiplexer, String redisKey)
        {
            _IConnectionMultiplexer = IConnectionMultiplexer;
            _redisKey = redisKey;
        }

        private IDatabase getClient()
        {
            return _IConnectionMultiplexer.GetDatabase();
        }

        public Boolean save(TEntity entity)
        {
            var db = getClient();
            return db.HashSet(_redisKey, entity.id, JsonConvert.SerializeObject(entity));
        }

        public TEntity findById(String id)
        {
            var db = getClient();
            var result = db.HashGet(_redisKey, id);
            if (result == RedisValue.Null)
                return null;

            return JsonConvert.DeserializeObject<TEntity>(result);
        }

        public TEntity updateStatus(TEntity roulette)
        {
            save(roulette);

            return roulette;
        }

        public List<TEntity> findAll()
        {
            var db = getClient();
            var result = db.HashValues(_redisKey);
            if (result == null || result.Length == 0)
                return null;

            return JsonConvert.DeserializeObject<List<TEntity>>(JsonConvert.SerializeObject(result));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
        }
    }
}
