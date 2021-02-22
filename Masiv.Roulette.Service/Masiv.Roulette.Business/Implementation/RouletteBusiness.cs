using entities = Masiv.RouletteProject.Model.Entities;
using Masiv.RouletteProject.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using Masiv.RouletteProject.Model.Dto;
using Masiv.RouletteProject.Model.Enums;
using Masiv.RouletteProject.Model.Constants;
using System.Linq;
using Masiv.RouletteProject.Business.Contract;
using Masiv.RouletteProject.Business.Utilities;

namespace Masiv.RouletteProject.Business.Implementation
{
    public class RouletteBusiness : IRouletteBusiness
    {
        private readonly IRouletteRepository _IRouletteRepository;
        private readonly IUserRepository _IUserRepository;
        public RouletteBusiness(IRouletteRepository IRouletteRepository, IUserRepository IUserRepository)
        {
            _IRouletteRepository = IRouletteRepository;
            _IUserRepository = IUserRepository;
        }

        public String save(entities.Roulette roulette)
        {
            if (roulette == null)
            {
                roulette = new entities.Roulette();
                roulette.status = false;
            }
            roulette.id = Guid.NewGuid().ToString();
            _IRouletteRepository.save(roulette);

            return roulette.id;
        }

        public List<entities.Roulette> findAll()
        {
            return _IRouletteRepository.findAll();
        }

        public entities.Roulette wager(WagerRequest wagerRequest, String key, String userId)
        {
            entities.User user = _IUserRepository.findById(userId);
            TypeEnum type = (TypeEnum)isValidType(wagerRequest);
            entities.Roulette roulette = _IRouletteRepository.findById(key);
            wagerValidation(user, roulette, wagerRequest, type, key);
            validateScoreArray(roulette, buildRouletteBet(wagerRequest.number, wagerRequest.color, wagerRequest.money, type, user));
            _IRouletteRepository.save(roulette);

            return roulette;
        }

        private Boolean validateValueByType(int? number, String color, TypeEnum typeEnum)
        {
            if (typeEnum.Equals(TypeEnum.number))
                return number != null && number >= 0 && number <= 36;

            return color.Equals(KeyConstants.RED) || color.Equals(KeyConstants.BLACK);
        }

        private void validateScoreArray(entities.Roulette roulette, entities.Wager wager)
        {
            if (roulette.lstWager == null || roulette.lstWager.Count == 0)
            {
                List<entities.Wager> listWager = new List<entities.Wager>();
                listWager.Add(wager);
                roulette.lstWager = listWager;
            }
            else
            {
                Boolean isPresent = roulette.lstWager.Exists(x => x.user.id.Equals(wager.user.id));
                if (isPresent)
                    throw new Exception("Lo sentimos!. solo puedes apostar una vez");
                else
                    roulette.lstWager.Add(wager);
            }
        }

        private entities.Wager buildRouletteBet(int? number, String color, decimal money, TypeEnum typeEnum, entities.User user)
        {
            String value = typeEnum.Equals(TypeEnum.number) ?
                    number.ToString() :
                    color;
            var wager = new entities.Wager { value = value, cash = money, user = user };

            return wager;
        }

        public entities.Roulette opening(String key)
        {
            entities.Roulette roulette = _IRouletteRepository.findById(key);
            openingValidation(key, roulette);
            if (roulette.lstWager != null && !(roulette.lstWager.Count == 0))
                roulette.lstWager.Clear();
            roulette.status = true;
            _IRouletteRepository.updateStatus(roulette);

            return roulette;
        }

        public Dictionary<String, WagerResponse> closing(String key)
        {
            entities.Roulette roulette = _IRouletteRepository.findById(key);
            closingValidation(key, roulette);
            roulette.status = false;
            int resultNumber = RandomValueGenerator.generateRandomNumber();
            _IRouletteRepository.updateStatus(roulette);

            return buildClose(roulette, resultNumber);
        }

        public Dictionary<String, WagerResponse> buildClose(entities.Roulette roulette, int resultNumber)
        {
            List<entities.Wager> winners = new List<entities.Wager>();
            List<entities.Wager> losers = new List<entities.Wager>();
            ColorEnum colorEnum = ColorEnum.negro;
            if ((resultNumber % 2) == 0) colorEnum = ColorEnum.rojo;
            foreach (var wager in roulette.lstWager)
                buildListResults(resultNumber, winners, losers, colorEnum, wager);
            Dictionary<String, WagerResponse> result = new Dictionary<String, WagerResponse>();
            result.Add("result", new WagerResponse
            {
                resultNumber = resultNumber,
                resultColor = colorEnum.ToString(),
                totalWinners = winners.Count,
                totalLosers = losers.Count,
                lstWagerWinners = winners,
                lstWagerLosers = losers
            });

            return result;
        }

        private void buildListResults(int resultNumber, List<entities.Wager> winners, List<entities.Wager> losers,
                                 ColorEnum colorEnum, entities.Wager wager)
        {
            if (wager.value.Equals(colorEnum.ToString()) || wager.value.Equals(resultNumber.ToString()))
            {
                wager.user.balance = (wager.user.balance + wager.cash);
                winners.Add(wager);
            }
            else
            {
                wager.user.balance = (wager.user.balance - wager.cash);
                losers.Add(wager);
            }
            _IUserRepository.save(wager.user);
        }

        private void wagerValidation(entities.User user, entities.Roulette roulette, WagerRequest wagerRequest, TypeEnum type, String key)
        {
            if (user == null)
                throw new Exception("Usuario no encontrado");
            if (roulette == null)
                throw new Exception("Ruleta no encontrada");
            if (roulette.status == false)
                throw new Exception("Ruleta cerrada");
            if (user.balance < wagerRequest.money)
                throw new Exception("No tiene fondos suficientes");
            if (!(wagerRequest.money >= 0 && wagerRequest.money <= 10000))
                throw new Exception("No se encuentra en el rango de dinero permitido de 0 a 10000");
            if (!validateValueByType(wagerRequest.number, wagerRequest.color, type))
                throw new Exception("Valores ingresados inválidos");
            if (key == null)
                throw new Exception("Llave no enviada");
        }

        private Enum isValidType(WagerRequest wagerRequest)
        {
            if (wagerRequest.number == null && String.IsNullOrEmpty(wagerRequest.color))
                throw new Exception("Color y numero inválidos");
            else if (!String.IsNullOrEmpty(wagerRequest.color))
                return TypeEnum.color;
            else if (wagerRequest.number != null)
                return TypeEnum.number;
            throw new Exception("Valores ingresados no correctos");
        }

        private void openingValidation(String key, entities.Roulette roulette)
        {
            if (key == null)
                throw new Exception("Llave vacía");
            if (roulette == null)
                throw new Exception("Ruleta inexistente");
            if (roulette.status == true)
                throw new Exception("La ruleta ya estaba vacía");
        }

        private void closingValidation(String key, entities.Roulette roulette)
        {
            if (key == null)
                throw new Exception("llave vacía");
            if (roulette == null)
                throw new Exception("Ruleta inexistente");
            if (roulette.status == false)
                throw new Exception("La ruleta ya estaba cerrada");
        }

        public void Dispose()
        {
            _IRouletteRepository.Dispose();
            _IUserRepository.Dispose();
        }
    }
}
