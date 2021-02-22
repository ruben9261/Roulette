using Masiv.RouletteProject.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using entities = Masiv.RouletteProject.Model.Entities;

namespace Masiv.RouletteProject.Business.Contract
{
    public interface IRouletteBusiness : IDisposable
    {
        String save(entities.Roulette roulette);
        List<entities.Roulette> findAll();
        entities.Roulette wager(WagerRequest wagerRequest, String key, String userId);
        entities.Roulette opening(String key);
    }
}
