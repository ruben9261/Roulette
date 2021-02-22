using Masiv.RouletteProject.Business.Contract;
using Masiv.RouletteProject.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using entities = Masiv.RouletteProject.Model.Entities;

namespace Masiv.RouletteProject.Business.Implementation
{
    public class UserBusiness: IUserBusiness
    {
        private readonly IUserRepository _IUserRepository;
        public UserBusiness(IUserRepository IUserRepository)
        {
            _IUserRepository = IUserRepository;
        }

        public String save(entities.User user)
        {
            user.id = Guid.NewGuid().ToString();
            _IUserRepository.save(user);

            return user.id;
        }

        public List<entities.User> findAll()
        {
            return _IUserRepository.findAll();
        }

        public void Dispose()
        {
            _IUserRepository.Dispose();
        }
    }
}
