using System;
using System.Collections.Generic;
using System.Text;

namespace Masiv.RouletteProject.Repository.Contract
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Boolean save(TEntity entity);
        TEntity findById(String id);
        TEntity updateStatus(TEntity roulette);
        List<TEntity> findAll();
    }
}
