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
    public class RouletteController : Controller
    {
        private readonly IRouletteBusiness _IRouletteBusiness;
        public RouletteController(IRouletteBusiness IRouletteBusiness)
        {
            _IRouletteBusiness = IRouletteBusiness;
        }

        [HttpPost()]
        Response<String> create([FromBody] entities.Roulette roulette)
        {
            Response<String> response = new Response<String>();
            String id;
            try
            {
                id = _IRouletteBusiness.save(roulette);
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
        List<entities.Roulette> list()
        {
            return _IRouletteBusiness.findAll();
        }

        [HttpGet("opening/{id}")]
        Response<entities.Roulette> opening([FromRoute] String id)
        {
            entities.Roulette roulette = null;
            Response<entities.Roulette> response = new Response<entities.Roulette>();
            try
            {
                roulette = _IRouletteBusiness.opening(id);
                response.buildResponseOk(roulette, String.Empty);
                return response;
            }
            catch (Exception e)
            {
                response.buildResponseError(e.Message);
            }

            return response;
        }

        [HttpGet("closing/{id}")]
        Response<entities.Roulette> closing([FromRoute] String id)
        {
            entities.Roulette roulette = null;
            Response<entities.Roulette> response = new Response<entities.Roulette>();
            try
            {
                roulette = _IRouletteBusiness.opening(id);
                response.buildResponseOk(roulette, String.Empty);
                return response;
            }
            catch (Exception e)
            {
                response.buildResponseError(e.Message);
            }

            return response;
        }

        [HttpGet("wager/{id}")]
        Response<entities.Roulette> wager([FromRoute] String id, [FromBody] WagerRequest wagerRequest, [FromHeader] String userId)
        {
            entities.Roulette roulette = null;
            Response<entities.Roulette> response = new Response<entities.Roulette>();
            try
            {
                roulette = _IRouletteBusiness.wager(wagerRequest, id, userId);
                response.buildResponseOk(roulette, String.Empty);
                return response;
            }
            catch (Exception e)
            {
                response.buildResponseError(e.Message);
            }

            return response;
        }

    }
}
