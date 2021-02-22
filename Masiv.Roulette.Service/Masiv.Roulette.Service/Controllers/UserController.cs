using Masiv.RouletteProject.Business.Contract;
using Masiv.RouletteProject.Model.Base;
using Masiv.RouletteProject.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entities = Masiv.RouletteProject.Model.Entities;

namespace Masiv.RouletteProject.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _IUserBusiness;
        public UserController(IUserBusiness IUserBusiness)
        {
            _IUserBusiness = IUserBusiness;
        }

        [HttpPost()]
        Response<String> create([FromBody] entities.User user)
        {
            Response<String> response = new Response<String>();
            String id;
            try
            {
                id = _IUserBusiness.save(user);
                response.buildResponseOk(id, String.Empty);
                return response;
            }
            catch (Exception e)
            {
                response.buildResponseError(e.Message);
            }

            return response;
        }

        [HttpGet()]
        List<entities.User> list()
        {
            return _IUserBusiness.findAll();
        }

    }
}
