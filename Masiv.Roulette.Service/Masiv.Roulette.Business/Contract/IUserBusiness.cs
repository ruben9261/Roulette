using Masiv.RouletteProject.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using entities = Masiv.RouletteProject.Model.Entities;

namespace Masiv.RouletteProject.Business.Contract
{
    public interface IUserBusiness : IDisposable
    {
        String save(entities.User user);
        List<entities.User> findAll();
    }
}
